//------------------------------------------------------------------------------
//	文件名称：System\Data\Cache\MemoryDB.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.ORM;
using System.Serialization;
using System.Web;
namespace System.Data {
    internal partial class MemoryDB {
        private static readonly ILog logger = LogManager.GetLogger( typeof( MemoryDB ) );
        private static IDictionary objectList = Hashtable.Synchronized( new Hashtable() );
        private static IDictionary indexList = Hashtable.Synchronized( new Hashtable() );
        public static IDictionary GetObjectsMap() {
            return objectList;
        }
        public static IDictionary GetIndexMap() {
            return indexList;
        }
        private static Object objLock = new object();
        private static Object chkLock = new object();
        private static IList GetObjectsByName( Type t ) {
            if (isCheckFileDB( t )) {
                lock (chkLock) {
                    if (isCheckFileDB( t )) {
                        loadDataFromFile( t );
                        _hasCheckedFileDB[t] = true;
                    }
                }
            }
            return (objectList[t.FullName] as IList);
        }
        private static Hashtable _hasCheckedFileDB = new Hashtable();
        private static Boolean isCheckFileDB( Type t ) {
            if (_hasCheckedFileDB[t] == null) return true;
            return false;
        }
        private static void UpdateObjects( String key, IList list ) {
            objectList[key] = list;
        }
        internal static CacheObject FindById( Type t, int id ) {
            IList list = GetObjectsByName( t );
            if (list.Count > 0) {
                int objIndex = getIndex( t.FullName, id );
                if (objIndex >= 0 && objIndex < list.Count) {
                    return list[objIndex] as CacheObject;
                }
            }
            return null;
        }
        internal static IList FindBy( Type t, String propertyName, Object val ) {
            String propertyKey = getPropertyKey( t.FullName, propertyName );
            NameValueCollection valueCollection = getValueCollection( propertyKey );
            String ids = valueCollection[val.ToString()];
            if (strUtil.IsNullOrEmpty( ids )) return new ArrayList();
            IList results = new ArrayList();
            String[] arrItem = ids.Split( ',' );
            foreach (String strId in arrItem) {
                int id = cvt.ToInt( strId );
                if (id < 0) continue;
                CacheObject obj = FindById( t, id );
                if (obj != null) results.Add( obj );
            }
            return results;
        }
        internal static IList FindAll( Type t ) {
            return new ArrayList( GetObjectsByName( t ) );
        }
        internal static void Insert( CacheObject obj ) {
            Type t = obj.GetType();
            String _typeFullName = t.FullName;
            IList list = FindAll( t );
            obj.Id = getNextId( list );
            int index = list.Add( obj );
            addIdIndex( _typeFullName, obj.Id, index );
            UpdateObjects( _typeFullName, list );
            makeIndexByInsert( obj );
            if (isInMemory( t )) return;
            Serialize( t );
        }
        internal static void InsertByIndex( CacheObject obj, String propertyName, Object pValue ) {
            Type t = obj.GetType();
            String _typeFullName = t.FullName;
            IList list = FindAll( t );
            obj.Id = getNextId( list );
            int index = list.Add( obj );
            addIdIndex( _typeFullName, obj.Id, index );
            UpdateObjects( _typeFullName, list );
            makeIndexByInsert( obj, propertyName, pValue );
            if (isInMemory( t )) return;
            Serialize( t );
        }
        internal static void InsertByIndex( CacheObject obj, Dictionary<String, Object> dic ) {
            Type t = obj.GetType();
            String _typeFullName = t.FullName;
            IList list = FindAll( t );
            obj.Id = getNextId( list );
            int index = list.Add( obj );
            addIdIndex( _typeFullName, obj.Id, index );
            UpdateObjects( _typeFullName, list );
            foreach (KeyValuePair<String, Object> kv in dic) {
                makeIndexByInsert( obj, kv.Key, kv.Value );
            }
            if (isInMemory( t )) return;
            Serialize( t );
        }
        internal static Result Update( CacheObject obj ) {
            Type t = obj.GetType();
            makeIndexByUpdate( obj );
            if (isInMemory( t )) return new Result();
            try {
                Serialize( t );
                return new Result();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        internal static Result updateByIndex( CacheObject obj, Dictionary<String, Object> dic ) {
            Type t = obj.GetType();
            makeIndexByUpdate( obj );
            if (isInMemory( t )) return new Result();
            try {
                Serialize( t );
                return new Result();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        internal static void Delete( CacheObject obj ) {
            Type t = obj.GetType();
            String _typeFullName = t.FullName;
            makeIndexByDelete( obj );
            IList list = FindAll( t );
            list.Remove( obj );
            UpdateObjects( _typeFullName, list );
            deleteIdIndex( _typeFullName, obj.Id );
            if (isInMemory( t )) return;
            Serialize( t, list );
        }
        private static int getNextId( IList list ) {
            if (list.Count == 0) return 1;
            CacheObject preObject = list[list.Count - 1] as CacheObject;
            return preObject.Id + 1;
        }
        private static Boolean isInMemory( Type t ) {
            return rft.GetAttribute( t, typeof( NotSaveAttribute ) ) != null;
        }
        private static Object objIndexLock = new object();
        private static Object objIndexLockInsert = new object();
        private static Object objIndexLockUpdate = new object();
        private static Object objIndexLockDelete = new object();
        private static void makeIndexByInsert( CacheObject cacheObject, String propertyName, Object pValue ) {
            Type t = cacheObject.GetType();
            String propertyKey = getPropertyKey( t.FullName, propertyName );
            lock (objIndexLock) {
                NameValueCollection valueCollection = getValueCollection( propertyKey );
                valueCollection.Add( pValue.ToString(), cacheObject.Id.ToString() );
                indexList[propertyKey] = valueCollection;
            }
        }
        private static void makeIndexByInsert( CacheObject cacheObject ) {
            Type t = cacheObject.GetType();
            PropertyInfo[] properties = getProperties( t );
            foreach (PropertyInfo p in properties) {
                String propertyKey = getPropertyKey( t.FullName, p.Name );
                lock (objIndexLockInsert) {
                    NameValueCollection valueCollection = getValueCollection( propertyKey );
                    addNewValueMap( valueCollection, cacheObject, p );
                }
            }
        }
        private static void makeIndexByUpdate( CacheObject cacheObject ) {
            Type t = cacheObject.GetType();
            PropertyInfo[] properties = getProperties( t );
            foreach (PropertyInfo p in properties) {
                String propertyKey = getPropertyKey( t.FullName, p.Name );
                lock (objIndexLockUpdate) {
                    NameValueCollection valueCollection = getValueCollection( propertyKey );
                    deleteOldValueIdMap( valueCollection, cacheObject.Id );
                    addNewValueMap( valueCollection, cacheObject, p );
                }
            }
        }
        private static void makeIndexByUpdate( CacheObject cacheObject, String propertyName, Object pValue ) {
            Type t = cacheObject.GetType();
            String propertyKey = getPropertyKey( t.FullName, propertyName );
            lock (objIndexLockUpdate) {
                NameValueCollection valueCollection = getValueCollection( propertyKey );
                deleteOldValueIdMap( valueCollection, cacheObject.Id );
                valueCollection.Add( pValue.ToString(), cacheObject.Id.ToString() );
                indexList[propertyKey] = valueCollection;
            }
        }
        private static void makeIndexByDelete( CacheObject cacheObject ) {
            Type t = cacheObject.GetType();
            PropertyInfo[] properties = getProperties( t );
            foreach (PropertyInfo p in properties) {
                String propertyKey = getPropertyKey( t.FullName, p.Name );
                lock (objIndexLockDelete) {
                    NameValueCollection valueCollection = getValueCollection( propertyKey );
                    deleteOldValueIdMap( valueCollection, cacheObject.Id );
                }
            }
        }
        private static PropertyInfo[] getProperties( Type t ) {
            return t.GetProperties( BindingFlags.Public | BindingFlags.Instance );
        }
        private static NameValueCollection getValueCollection( String propertyKey ) {
            NameValueCollection valueCollection = indexList[propertyKey] as NameValueCollection;
            if (valueCollection == null) valueCollection = new NameValueCollection();
            return valueCollection;
        }
        private static void addNewValueMap( NameValueCollection valueCollection, CacheObject cacheObject, PropertyInfo p ) {
            Attribute attr = rft.GetAttribute( p, typeof( NotSaveAttribute ) );
            if (attr != null) return;
            String propertyKey = getPropertyKey( cacheObject.GetType().FullName, p.Name );
            Object pValue = rft.GetPropertyValue( cacheObject, p.Name );
            if (pValue == null || strUtil.IsNullOrEmpty( pValue.ToString() )) return;
            valueCollection.Add( pValue.ToString(), cacheObject.Id.ToString() );
            indexList[propertyKey] = valueCollection;
        }
        // TODO 优化
        private static void deleteOldValueIdMap( NameValueCollection valueCollection, int oid ) {
            foreach (String key in valueCollection.AllKeys) {
                String val = valueCollection[key];
                String[] arrItem = val.Split( ',' );
                StringBuilder result = new StringBuilder();
                foreach (String strId in arrItem) {
                    int id = cvt.ToInt( strId );
                    if (id == oid) continue;
                    result.Append( strId );
                    result.Append( "," );
                }
                String resultStr = result.ToString();
                if (strUtil.HasText( resultStr ))
                    valueCollection[key] = resultStr.Trim().TrimEnd( ',' );
                else
                    valueCollection.Remove( key );
            }
        }
        private static String getPropertyKey( String typeFullName, String propertyName ) {
            return typeFullName + "_" + propertyName;
        }
        private static IDictionary GetIdIndexMap( String key ) {
            if (objectList[key] == null) {
                objectList[key] = new Hashtable();
            }
            return (objectList[key] as IDictionary);
        }
        private static void UpdateIdIndexMap( String key, IDictionary map ) {
            objectList[key] = map;
        }
        private static void clearIdIndexMap( String key ) {
            objectList.Remove( key );
        }
        private static void addIdIndex( String typeFullName, int oid, int index ) {
            String key = getIdIndexMapKey( typeFullName );
            IDictionary indexMap = GetIdIndexMap( key );
            indexMap[oid] = index;
            UpdateIdIndexMap( key, indexMap );
        }
        private static void deleteIdIndex( String typeFullName, int oid ) {
            String key = getIdIndexMapKey( typeFullName );
            clearIdIndexMap( key );
            IList results = objectList[typeFullName] as IList;
            foreach (CacheObject obj in results) {
                addIdIndex( typeFullName, obj.Id, results.IndexOf( obj ) );
            }
            IDictionary indexMap = GetIdIndexMap( key );
            UpdateIdIndexMap( key, indexMap );
        }
        private static int getIndex( String typeFullName, int oid ) {
            int result = -1;
            Object objIndex = GetIdIndexMap( getIdIndexMapKey( typeFullName ) )[oid];
            if (objIndex != null) {
                result = (int)objIndex;
            }
            return result;
        }
        private static String getIdIndexMapKey( String typeFullName ) {
            return String.Format( "{0}_oid_index", typeFullName );
        }
        private static String getCachePath( Type t ) {
            if (SystemInfo.IsWeb == false) {
                return getCacheFileName( t.FullName );
            }
            return getWebCacheFileName( t.FullName );
        }
        private static String getCacheFileName( String name ) {
            return PathHelper.CombineAbs( new String[] {
                AppDomain.CurrentDomain.BaseDirectory,
                cfgHelper.FrameworkRoot,
                "data",
                name + fileExt
            } );
        }
        private static String getWebCacheFileName( String name ) {
            String rpath = strUtil.Join( cfgHelper.FrameworkRoot, "data" );
            rpath = strUtil.Join( rpath, name + fileExt );
            return PathHelper.Map( rpath );
        }
        private static readonly String fileExt = ".config";

        private static void Serialize(Type t)
        {
            Serialize(t, GetObjectsByName(t));
        }
        private static void Serialize(Type t, IList list)
        {
            String target = SimpleJsonString.ConvertList(list);
            if (strUtil.IsNullOrEmpty(target)) return;
            String absolutePath = getCachePath(t);
            lock (objLock)
            {
                System.IO.FileEx.Write(absolutePath, target);
            }
        }
        private static void loadDataFromFile(Type t)
        {
            if (System.IO.File.Exists(getCachePath(t)))
            {
                IList list = getListWithIndex(System.IO.FileEx.Read(getCachePath(t)), t);
                objectList[t.FullName] = list;
            }
            else
            {
                objectList[t.FullName] = new ArrayList();
                //file.Write(getCachePath(t), Json.ToStringEx(objectList));
            }
        }
        private static IList getListWithIndex(String jsonString, Type t)
        {
            IList list = new ArrayList();
            if (strUtil.IsNullOrEmpty(jsonString)) return list;
            List<object> lists = JsonParser.Parse(jsonString) as List<object>;
            if (lists != null)
            {
                foreach (Dictionary<String, object> map in lists)
                {
                    CacheObject obj = JSON.setValueToObject(t, map) as CacheObject;
                    int index = list.Add(obj);
                    addIdIndex(t.FullName, obj.Id, index);
                    makeIndexByInsert(obj);
                }
            }
            return list;
        }
    }
}