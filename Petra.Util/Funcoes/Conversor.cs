using Dardani.DAO.NH;
using NHibernate;
using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Petra.Util.Funcoes
{
    public class Conversor
    {
        public static void Converter(Object origem, Object destino, ISession sessao = null)
        {
            Type t = origem.GetType();
            foreach (PropertyInfo piOrigem in t.GetProperties())
            {
                object[] att = piOrigem.GetCustomAttributes(typeof(ConverterEntidadeAttribute),false);
                Object value = piOrigem.GetValue(origem);
                String nomeCampo, fileName, classeDAO;
                foreach (ConverterEntidadeAttribute a in att)
                {
                    nomeCampo = String.IsNullOrEmpty(a.Campo) ? piOrigem.Name : a.Campo;
                    classeDAO = a.ClasseDao;
                    fileName = a.FileName;

                    if (!String.IsNullOrEmpty(a.NomeEntidade) && (sessao != null) ){
                        var objeto = sessao.Get(a.NomeEntidade, value);
                        value = objeto;

                        nomeCampo = String.IsNullOrEmpty(a.Campo) ? a.NomeEntidade : a.Campo;
                        
                        
                        /*
                        if (!String.IsNullOrEmpty(a.Campo))
                        {
                            nomeCampo = a.NomeEntidade;
                        }
                         */ 
                    }
                    

                    Object objetoAlvo = destino;
                    if (!String.IsNullOrEmpty(classeDAO) && !String.IsNullOrEmpty(nomeCampo))
                    {

                        //string pa = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        string dir = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin"); // note: don't use CurrentEntryAssembly or anything like that.



                        //String classeDAO = "Dardani.EDU.BO.NH.SalaDAO";
                        //String fileName = "Dardani.EDU.BO.dll";
                        // Olddir

                        FileInfo fileInfo = new FileInfo(dir+@"\" + fileName);
                        Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
                        //Assembly assembly = Assembly.LoadFile(fileName);
                        Type tdao = assembly.GetType(classeDAO);

                        


                        Type[] paramTypes = new Type[1];
                        paramTypes[0] = Type.GetType("System.Int32");
                        MethodInfo GetByIdInfo = t.GetMethod("GetById", paramTypes);
                        Object dao = Activator.CreateInstance(tdao);


                        /*
                        Type xtipo = destino.GetType();
                        PropertyInfo xpiDestino = xtipo.GetProperty(a.Objeto);
                        if (xpiDestino != null)
                        {
                            objetoAlvo = xpiDestino.GetValue(destino);
                            //SetarPropriedade(objetoAlvo, nomeCampo, value);
                        }
                        */

                        sessao.Get(classeDAO, new Object[1]);
                        
                        Object[] parameters = new Object[1];
                        parameters[0] = value;
                        Object retorno = GetByIdInfo.Invoke(dao, parameters);


                        //SetarPropriedade(objetoAlvo, nomeCampo, value);



                        //Type actualType = Type.GetType("Ensino"); 


                        //Type openGenericClass = Type.GetType(nomeDao);
                        //openGenericClass.MakeGenericType(typeof(actualType));




                        //GenericDAO<typeof()> dao = Activator.CreateInstance(openGenericClass);

                    } else if (!String.IsNullOrEmpty(a.Objeto))
                    {
                        Type tipo = destino.GetType();
                        PropertyInfo piDestino = tipo.GetProperty(a.Objeto);
                        if (piDestino != null)
                        {
                            objetoAlvo = piDestino.GetValue(destino);
                            SetarPropriedade(objetoAlvo, nomeCampo, value);
                        }
                    }
                    else
                    {
                        SetarPropriedade(objetoAlvo, nomeCampo, value);
                    }
                }
            }
        }

        private static void SetarPropriedade(Object objetoAlvo, string campo, Object valor)
        {
            Type tipo = objetoAlvo.GetType();
            PropertyInfo piDestino = tipo.GetProperty(campo);

            if ((objetoAlvo != null) && (piDestino != null))
            {
                piDestino.SetValue(objetoAlvo, valor);
            }
        }


    }
}
