using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Relevamiento.Clases
{
    public class EstadosRel
    {
        public ItrisRelevamientoEntity relevamiento { get; set; }
        public List<ItrisComercioArticulo> comercios { get; set; }
        public string codigoRequest { get; set; }
        public bool req_estado { get; set; }
        public ERP_EMPRESAS Empresa { get; set; }
        public Provincia provincia { get; set; }
    }
    [DataContract]
    public class ItrisRelevamientoEntity
    {
		[DataMember(EmitDefaultValue = false)]
		public int ID { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string FK_ERP_EMPRESAS { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_ERP_ASESORES { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string FECHA { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string CODIGO { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public bool ENVIADO_POR_MAIL { get; set; }
	}

	[DataContract]
	public class ItrisComercioEntity
    {
		[DataMember(EmitDefaultValue = false)]
		public int ID { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_TIP_COM { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string NOMBRE { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string CALLE { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string NUMERO { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_ERP_LOCALIDADES { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_ERP_PROVINCIAS { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string LATITUD { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public string LONGITUD { get; set; }
	}

	[DataContract]
	public class ItrisRelevamientoArticuloEntity
    {
		[DataMember(EmitDefaultValue = false)]
		public int ID { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_RELEVAMIENTO { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_ARTICULOS { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public int FK_COMERCIO { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public bool EXISTE { get; set; }

		[DataMember(EmitDefaultValue = false)]
		public double PRECIO { get; set; }
	}

	public class ItrisComercioArticulo
    {
        public ItrisComercioEntity comercio { get; set; }
        public List<ItrisRelevamientoArticuloEntity> relevamientoArticulo { get; set; }
    }

    public class ItrisPlanillaEntity
    {
        public ItrisRelevamientoEntity relevamiento { get; set; }
        public List<ItrisComercioArticulo> comercios { get; set; }
        public string codigoRequest { get; set; }
    }

    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string NumeroImei { get; set; }
    }
    public class Provincia
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Prov { get; set; }

    }

    public class TipoLocal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Tipo { get; set; }

    }

    //public class Local
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int Id { get; set; }
    //    public string Provincia { get; set; }
    //    public string TipoLocal { get; set; }
    //    public string Distribuidor { get; set; }
    //    public string Nombre { get; set; }
    //    public string Calle { get; set; }
    //    public string Numero { get; set; }
    //    public string Localidad { get; set; }

    //    public string Direccion
    //    {
    //        get
    //        {
    //            return String.Format("{0} {1}, ({2})", Calle, Numero, Localidad);
    //        }
    //    }
    //    public string FormattedText
    //    {
    //        get
    //        {
    //            return String.Format("Local: {0} - Direccion: {1}, ({2})", Nombre, Direccion, Provincia);
    //        }
    //    }
    //    //[OneToMany(CascadeOperations = CascadeOperation.CascadeInsert)]
    //    //public List<ListaProductos> ListadoProductos { get; set; }

    //}

    //public class TipoProductos
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int Id { get; set; }

    //    public string TipoProducto { get; set; }

    //}

    public class ListaProductos
    {
        [PrimaryKey, AutoIncrement]
       // [ForeignKey(typeof(Local))]
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Precio { get; set; }
        public bool Existe { get; set; }
        public int TipoProducto { get; set; }
    }

    public class Relevado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Id del dato
        public string TipoLocal { get; set; } // tipo de local relevado
        public string Direccion { get; set; } // Direccion del local relevado
        public string Provincia { get; set; } // PRovincia del comercio relevado
        public string NombreDistribuidor { get; set; } // Nombre del distribuidor
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public DateTime FechaRelevado { get; set; }
        public bool Status { get; set; }

    }

    public class ERP_ASESORES{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string c_EMAIL { get; set; }
        public string c_IMEI { get; set; }
        public bool c_IMEI_ADMIN { get; set; }
    }

    public class _TIP_COM
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class _TIP_ART
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class ERP_EMPRESAS
    {
        [PrimaryKey]
        public string ID { get; set; }
        public int FK_ERP_ASESORES { get; set; }
        public int FK_ERP_ASESORES2 { get; set; }
        public int FK_ERP_ASESORES3 { get; set; }
        public string NOM_FANTASIA { get; set; }
        public string Z_FK_ERP_PROVINCIAS { get; set; }
        public string Z_FK_ERP_PARTIDOS { get; set; }
        public string Z_FK_ERP_LOCALIDADES { get; set; }
        public string FormattedText
        {
            get
            {
                return String.Format("{0}: {1}", NOM_FANTASIA, ID);
            }
        }
    }

    public class _COMERCIO
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(_TIP_COM))]
        public int FK_TIP_COM { get; set; }
        [ForeignKey(typeof(ERP_LOCALIDADES))]
        public string FK_ERP_LOCALIDADES { get; set; }
        [ForeignKey(typeof(ERP_LOCALIDADES))]
        public int FK_ERP_PROVINCIAS { get; set; }
        public string NOMBRE { get; set; }
        public string CALLE { get; set; }
        public string NUMERO { get; set; }
        public int LATITUD { get; set; }
        public int LONGITUD { get; set; }

    }

    public class _ARTICULOS
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(_TIP_ART))]
        public int FK_TIP_ART { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ARTICULO_PROPIO { get; set; }
        public double Precio { get; set; }
        public bool Existe { get; set; }
    }

public class ERP_LOCALIDADES
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
        public int FK_ERP_PARTIDOS { get; set; }
        public string Z_FK_ERP_PARTIDOS { get; set; }
        public int FK_ERP_PROVINCIAS { get; set; }
        public string Z_FK_ERP_PROVINCIAS { get; set; }
    }
}
