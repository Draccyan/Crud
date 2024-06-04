using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Datos.Exceptions
{
    public static class ExceptionHelper
    {
        public static Exception Handle(Exception ex)
        {
            if (ex is DbUpdateException dbUpdateException)
            {
                var exception = HandleDbUpdateException(dbUpdateException);
                return exception;
            }
            //
            return ex;
        }

        private static Exception HandleDbUpdateException(DbUpdateException dbu)
        {

            var lastError = dbu.Message;
            Exception ex = dbu;
            while (true)
            {
                ex = ex.InnerException;
                if (ex == null)
                {
                    break;
                }
                lastError = ex.Message;
            }
            var builder = new StringBuilder($"Error:{dbu.Message}. Inner:{lastError}");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("En la entidad:{0}.", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error obteniendo informacion de la Excpecion: " + e.ToString());
            }

            var message = builder.ToString();
            return new Exception(message, dbu);
        }

    }
}