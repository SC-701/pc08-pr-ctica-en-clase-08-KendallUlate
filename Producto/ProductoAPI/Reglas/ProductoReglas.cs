using Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Interfaces.Servicios;


namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoDeCambioServicio _tipoDeCambioServicio;

        public ProductoReglas(ITipoDeCambioServicio tipoDeCambioServicio)
        {
            _tipoDeCambioServicio = tipoDeCambioServicio;
        }

        public async Task<decimal> CalcularPrecioUSD(decimal precioCRC)
        {
            var tipoCambio = await _tipoDeCambioServicio.Obtener();

            if (tipoCambio <= 0)
                throw new Exception("No se obtuvo un tipo de cambio válido");

            return Math.Round(precioCRC / tipoCambio, 2);
        }
    }
}