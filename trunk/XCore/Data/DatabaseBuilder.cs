using System;
using System.Data;
using System.IO;
using System.Web;

using System.Reflection;
using System.Web;

namespace System.Data {


    internal class DatabaseBuilder {

        public static String ConnectionStringPrefix = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        private static readonly ILog logger = LogManager.GetLogger( typeof( DatabaseBuilder ) );

        public static String BuildAccessDb4o() {
            String dbPath = getDbPath();
            BuildAccessDb4o( dbPath );
            return dbPath;
        }
        public static String BuildAccessDb4oWithPrefix()
        {
            return ConnectionStringPrefix + getDbPath();
        }

        public static String BuildAccessDb4o(String dbPath)
        {
            String str = ConnectionStringPrefix + dbPath;
            logger.Info( "creating database : " + str );
            Object instanceFromProgId = ReflectionUtil.GetInstanceFromProgId( "ADOX.Catalog" );
            try {
                ReflectionUtil.CallMethod(instanceFromProgId, "Create", new object[] { str });
                //ReflectionUtil.CallMethod(instanceFromProgId, "Create", new object[] { ConnectionStringPrefix + PathHelper.Map(dbPath) });
            }
            catch (Exception exception) {
                logger.Info( "creating database error : " + exception.Message );
                LogManager.Flush();
                throw exception;
            }
            logger.Info( "create database ok" );
            return str;
        }

        //public static void Compact( String dbPath ) {
        //    if (!File.Exists( dbPath )) {
        //        throw new Exception( "database not found" );
        //    }
        //    IDbConnection connection = DbContext.getConnection();
        //    if ((connection != null) && (connection.State == ConnectionState.Open)) {
        //        connection.Close();
        //    }
        //    String sourceFileName = dbPath + ".bak";
        //    ReflectionUtil.CallMethod( ReflectionUtil.GetInstanceFromProgId( "JRO.JetEngine" ), "CompactDatabase", new object[] { ConnectionStringPrefix + dbPath, ConnectionStringPrefix + sourceFileName } );
        //    File.Copy( sourceFileName, dbPath, true );
        //    File.Delete( sourceFileName );
        //}

        private static String getDbPath()
        {
            return "xcore.mdb";
            try
            {
                //String dbname = "xcore" + Guid.NewGuid().ToString().Replace("-", "") + ".mdb";
                string dbname = "xcore.mdb";
                String path = "";
                //path = PathHelper.Map(cfgHelper.FrameworkRoot + path);
                path = string.Format("{0}{1}/{2}/{3}", cfgHelper.FrameworkRoot, System.TemplateEngine.TeConfig.Instance.TemplateFolder, System.TemplateEngine.TeConfig.Instance.CurrentSkin, dbname);
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    path = cfgHelper.FrameworkRoot + dbname;
                }
                return path;
            }
            catch (Exception exception)
            {
                logger.Info("creating database error : " + exception.Message);
                LogManager.Flush();
                throw exception;
            }
        }

    }
}

