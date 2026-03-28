using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operadores

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            },
            commandType: CommandType.StoredProcedure

            );

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid id, ProductoRequest producto)
        {

            string query = "EditarProducto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(
                query,
                new
                {
                    Id = id,
                    IdSubCategoria = producto.IdSubCategoria,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CodigoBarras = producto.CodigoBarras
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            string query = @"EliminarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            },
            commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;

        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";

            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoDetalle>(
                query,
                new { Id = Id },
                commandType: CommandType.StoredProcedure);

            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task verificarProductoExiste(Guid Id)
        {
            ProductoDetalle? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("No se encontró el producto");
        }

        #endregion
    }
}
