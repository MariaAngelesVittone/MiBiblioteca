namespace Fase4Testing
{
    // Estos tests ya estan escritos: son la especificacion de
    // CuentaBancaria.cs. Fijate el patron AAA (Arrange-Act-Assert) que se
    // repite en todos: primero se prepara el escenario, despues se ejecuta
    // UNA accion, despues se verifica el resultado. Es el mismo patron
    // que usa Tests/MiBiblioteca.UnitTests/Services/AuthServiceTests.cs
    // en el proyecto principal.
    public class CuentaBancariaTests
    {
        [Fact]
        public void Constructor_AsignaElSaldoInicial()
        {
            // Arrange + Act (se mezclan porque construir el objeto ES la
            // accion que estamos probando)
            var cuenta = new CuentaBancaria(1000);

            // Assert
            Assert.Equal(1000, cuenta.Saldo);
        }

        [Fact]
        public void Depositar_SumaAlSaldo()
        {
            // Arrange
            var cuenta = new CuentaBancaria(1000);

            // Act
            cuenta.Depositar(500);

            // Assert
            Assert.Equal(1500, cuenta.Saldo);
        }

        [Fact]
        public void Depositar_ConMontoNegativo_LanzaArgumentException()
        {
            // Arrange
            var cuenta = new CuentaBancaria(1000);

            // Act + Assert: cuando lo que probamos es que se lanza una
            // excepcion, Act y Assert quedan juntos en la misma linea.
            Assert.Throws<ArgumentException>(() => cuenta.Depositar(-100));
        }

        [Fact]
        public void Depositar_ConMontoCero_LanzaArgumentExceptionYNoModificaElSaldo()
        {
            // Arrange
            var cuenta = new CuentaBancaria(1000);

            // Act + Assert
            Assert.Throws<ArgumentException>(() => cuenta.Depositar(0));

            // Assert extra: la excepcion no debe haber modificado el saldo.
            Assert.Equal(1000, cuenta.Saldo);
        }

        [Fact]
        public void Retirar_RestaDelSaldo()
        {
            // Arrange
            var cuenta = new CuentaBancaria(1000);

            // Act
            cuenta.Retirar(300);

            // Assert
            Assert.Equal(700, cuenta.Saldo);
        }

        [Fact]
        public void Retirar_ConSaldoInsuficiente_LanzaInvalidOperationExceptionYNoModificaElSaldo()
        {
            // Arrange
            var cuenta = new CuentaBancaria(100);

            // Act + Assert
            var excepcion = Assert.Throws<InvalidOperationException>(() => cuenta.Retirar(500));

            Assert.Equal("Saldo insuficiente.", excepcion.Message);
            Assert.Equal(100, cuenta.Saldo);
        }

        [Fact]
        public void TransferirA_MueveElMontoDeUnaCuentaAOtra()
        {
            // Arrange
            var origen = new CuentaBancaria(1000);
            var destino = new CuentaBancaria(200);

            // Act
            origen.TransferirA(destino, 300);

            // Assert
            Assert.Equal(700, origen.Saldo);
            Assert.Equal(500, destino.Saldo);
        }

        // ============================================================
        // TU TURNO
        // ============================================================
        // Los tests de arriba son la especificacion que necesitas para
        // implementar CuentaBancaria.cs. Una vez que todos esos pasen,
        // agrega VOS, como metodos [Fact] nuevos en esta misma clase, los
        // tests de estos dos escenarios (todavia no probados):
        //
        // 1. Retirar_ConMontoNegativo_LanzaArgumentException
        //    (mismo patron que Depositar_ConMontoNegativo_LanzaArgumentException)
        //
        // 2. TransferirA_ConSaldoInsuficiente_NoModificaNingunaDeLasDosCuentas
        //    (crea origen y destino, intenta transferir mas de lo que
        //    origen tiene, envolvé la llamada en Assert.Throws, y
        //    verificá que NINGUNA de las dos cuentas haya cambiado su
        //    saldo despues del intento fallido)
        //
        // No hay una "respuesta" que este archivo verifique en automatico
        // para esta parte - la practica es justamente escribir el test
        // y ver que efectivamente detecta el comportamiento correcto
        // (corriendolo contra una implementacion a proposito rota, por
        // ejemplo, para confirmar que el test SI falla cuando deberia).
    }
}
