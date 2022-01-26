using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_PERMISOS
    {
        public List<Permiso> Listar(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select P.idRol,P.NombreMenu from PERMISO P");
                    query.AppendLine("INNER JOIN ROL R ON R.IdRol = P.IdRol");
                    query.AppendLine("INNER JOIN USUARIO U ON u.IdRol = r.IdRol");
                    query.AppendLine("where u.IdUsuario = @idusuario");
                                                                               
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                ORol = new Rol() {IdRol = Convert.ToInt32(dr["IdRol"])},
                                NombreMenu = (dr["NombreMenu"]).ToString()
                            }) ;

                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();

                }
            }

            return lista;
        }

    }
}
}
