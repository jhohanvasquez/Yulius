//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoginApi.Datos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class BDFODUNReservasEntities : DbContext
    {
        public BDFODUNReservasEntities()
            : base("name=BDFODUNReservasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ApiConfiguraciones> ApiConfiguraciones { get; set; }
        public DbSet<ApiConfiguraciones_Rutas> ApiConfiguraciones_Rutas { get; set; }
    
        public virtual ObjectResult<pa_Api_Informacion_Result> pa_Api_Informacion(string pidentificador)
        {
            var pidentificadorParameter = pidentificador != null ?
                new ObjectParameter("pidentificador", pidentificador) :
                new ObjectParameter("pidentificador", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_Api_Informacion_Result>("pa_Api_Informacion", pidentificadorParameter);
        }
    }
}
