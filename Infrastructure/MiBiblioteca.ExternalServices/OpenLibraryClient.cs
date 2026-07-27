using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Services;

namespace MiBiblioteca.ExternalServices
{
    public class OpenLibraryClient : IBookMetadataProvider
    {
        public const string HttpClientName = "OpenLibraryClient";

        private readonly IHttpClientFactory _httpClientFactory;

        public OpenLibraryClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BookMetadataDto?> GetByIsbnAsync(string isbn)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);

            // La API de Open Library devuelve un diccionario donde la clave
            // es literalmente el string "ISBN:<isbn>", no el libro directo.
            var response = await client.GetAsync($"api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var parsed = JsonSerializer.Deserialize<Dictionary<string, OpenLibraryBook>>(json);

            if (parsed is null || !parsed.TryGetValue($"ISBN:{isbn}", out var book))
            {
                return null;
            }

            return new BookMetadataDto
            {
                Title = book.Title,
                Author = book.Authors?.FirstOrDefault()?.Name ?? "Desconocido",
                CoverUrl = book.Cover?.Medium,
                PublishedYear = ExtractYear(book.PublishDate)
            };
        }

        private static int? ExtractYear(string? publishDate)
        {
            if (string.IsNullOrWhiteSpace(publishDate)) return null;

            var match = Regex.Match(publishDate, @"\d{4}");
            return match.Success ? int.Parse(match.Value) : null;
        }

        // Clases privadas solo para deserializar el JSON de Open Library.
        // No son DTOs de nuestra API - por eso no viven en Application.
        private class OpenLibraryBook
        {
            [JsonPropertyName("title")]
            public string Title { get; set; } = string.Empty;

            [JsonPropertyName("authors")]
            public List<OpenLibraryAuthor>? Authors { get; set; }

            [JsonPropertyName("publish_date")]
            public string? PublishDate { get; set; }

            [JsonPropertyName("cover")]
            public OpenLibraryCover? Cover { get; set; }
        }

        private class OpenLibraryAuthor
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;
        }

        private class OpenLibraryCover
        {
            [JsonPropertyName("medium")]
            public string? Medium { get; set; }
        }
    }
}
