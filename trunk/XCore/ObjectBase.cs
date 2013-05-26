/*
 * Copyright 2012 www.xcenter.cn
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;
using System.Reflection;
using System.Serialization;

namespace System {

    /// <summary>
    /// ����ORM�е�����ģ�Ͷ���Ҫ�̳еĻ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ObjectBase<T> : IEntity, IComparable where T : ObjectBase<T> {

        private int _id;

        /// <summary>
        /// id ���������
        /// </summary>
        public int Id {
            get { return _id; }
            set { this.setId(value); _id = value; }
        }

        protected virtual void setId(int _id)
        {
        }

        /// <summary>
        /// ��ѯ��������
        /// </summary>
        /// <returns></returns>
        public static List<T> findAll() { return db.findAll<T>(); }

        /// <summary>
        /// ���� Id ��ѯ����
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static T findById(int oid) { return db.findById<T>(oid); }
        

        /// <summary>
        /// ����ָ���ֶλ�ȡ����
        /// </summary>
        /// <param name="Field">�ֶ�����varchar���ͣ�</param>
        /// <param name="Value">ֵ</param>
        /// <returns></returns>
        public static T findByField(string Field, string Value)
        {
            try
            {
                return db.find<T>(string.Format("{0}='{1}'", Field, Value)).first();
            }
            catch { return null; }
        }
        /// <summary>
        /// ����ָ���ֶ��б��ȡ����
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static T findByField(bool isOr, bool isLike, System.Collections.Hashtable ht)
        {
            try
            {
                StringBuilder where;
                if (isOr)
                {
                    where = new StringBuilder("1=0");
                }
                else
                {
                    where = new StringBuilder("1=1");
                }
                System.Collections.IDictionaryEnumerator ienum = ht.GetEnumerator();
                while (ienum.MoveNext())
                {
                    if (isOr)
                    {
                        where.AppendFormat(" or {0}='{1}'", ienum.Key, ienum.Value);
                    }
                    else
                    {
                        where.AppendFormat(" and {0}='{1}'", ienum.Key, ienum.Value);
                    }
                }
                return db.find<T>(where.ToString()).first();
            }
            catch { return null; }
        }
        /// <summary>
        /// ����ָ���ֶ��б��ȡ����
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static T findByField(bool isOr, bool isLike, params System.Web.KeyValue[] kvs)
        {
            try
            {
                StringBuilder where;
                if (isOr)
                {
                    where = new StringBuilder("1=0");
                }
                else
                {
                    where = new StringBuilder("1=1");
                }
                if (kvs == null)
                {
                    kvs = new System.Web.KeyValue[] { };
                }
                foreach (System.Web.KeyValue kv in kvs)
                {
                    if (isOr)
                    {
                        if (isLike)
                        {
                            where.AppendFormat(" or {0} like '%{1}%'", kv.Key, kv.Value);
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        if (isLike)
                        {
                            where.AppendFormat(" and {0} like '%{1}%'", kv.Key, kv.Value);
                        }
                        else
                        {
                            where.AppendFormat(" and {0}='{1}'", kv.Key, kv.Value);
                        }
                    }
                }
                return db.find<T>(where.ToString()).first();
            }
            catch { return null; }
        }

        /// <summary>
        /// ����ָ���ֶλ�ȡ�����б�
        /// </summary>
        /// <param name="Field">�ֶ�����varchar���ͣ�</param>
        /// <param name="Value">ֵ</param>
        /// <returns></returns>
        public static List<T> findListByField(string Field, string Value)
        {
            try
            {
                return db.find<T>(string.Format("{0}='{1}'", Field, Value)).list();
            }
            catch { return new List<T>(); }
        }

        /// <summary>
        /// ͳ�����е�������
        /// </summary>
        /// <returns></returns>
        public static int count() { return db.count<T>(); }

        /// <summary>
        /// ��������ͳ��������
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int count( String condition ) { return db.count<T>( condition ); }

        /// <summary>
        /// ���ݲ�ѯ����������һ����ѯ����һ�����ڲ�������ѯ��
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>���ز�ѯ����xQuery�����Խ�һ����������ֵ�����õ����</returns>
        public static xQuery<T> find( String condition ) { return db.find<T>( condition ); }

        /// <summary>
        /// ���ݲ�ѯ���������ط�ҳ���ݼ���
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns></returns>
        public static DataPage<T> findPage( String condition ) { return db.findPage<T>( condition ); }

        /// <summary>
        /// ���ݲ�ѯ������ÿҳ���������ط�ҳ���ݼ���
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <returns></returns>
        public static DataPage<T> findPage( String condition, int pageSize ) { return db.findPage<T>( condition, pageSize ); }

        /// <summary>
        /// �浵ģʽ��ҳ(Ĭ�ϰ��� order by Id asc ����)
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>��ҳ�����б�������ǰҳ���ܼ�¼������ҳ����</returns>
        public static DataPage<T> findPageArchive( String condition ) { return db.findPageArchive<T>( condition ); }

        /// <summary>
        /// �浵ģʽ��ҳ(Ĭ�ϰ��� order by Id asc ����)
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <returns>��ҳ�����б�������ǰҳ���ܼ�¼������ҳ����</returns>
        public static DataPage<T> findPageArchive( String condition, int pageSize ) { return db.findPageArchive<T>( condition, pageSize ); }


        /// <summary>
        /// ֱ��ʹ�� sql ����ѯ�����ض����б�
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> findBySql( String sql ) { return db.findBySql<T>( sql ); }

        /// <summary>
        /// ������������ݿ�
        /// </summary>
        /// <returns>����һ��������� Result��������������� Result �а���������Ϣ</returns>
        public Result insert() { return db.insert( this ); }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>����һ��������� Result��������������� Result �а���������Ϣ</returns>
        public Result update() { return db.update( this ); }

        /// <summary>
        /// ֻ�޸Ķ����ĳ���ض�����
        /// </summary>
        /// <param name="propertyName">��������</param>
        public void update( String propertyName ) { db.update( this, propertyName ); }

        /// <summary>
        /// ֻ�޸Ķ�����ض�����
        /// </summary>
        /// <param name="arrPropertyName">��Ҫ�޸ĵ����Ե�����</param>
        public void update( String[] arrPropertyName ) { db.update( this, arrPropertyName ); }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>������Ӱ�������</returns>
        public int delete() { return db.delete( this ); }

        /// <summary>
        /// �������¶���
        /// </summary>
        /// <param name="action">���µĲ���</param>
        /// <param name="condition">���µ�����</param>
        public static void updateBatch( String action, String condition ) { db.updateBatch<T>( action, condition ); }

        /// <summary>
        /// ���� Id ɾ������
        /// </summary>
        /// <param name="oid"></param>
        /// <returns>������Ӱ�������</returns>
        public static int delete( int oid ) { return db.delete<T>( oid ); }

        /// <summary>
        /// ����ɾ������
        /// </summary>
        /// <param name="condition">ɾ������</param>
        /// <returns>������Ӱ�������</returns>
        public static int deleteBatch( String condition ) { return db.deleteBatch<T>( condition ); }

        //------------------------------------- ����ʵ������ --------------------------------------------

        /// <summary>
        /// �����������ƻ�ȡ���Ե�ֵ
        /// </summary>
        /// <param name="propertyName">��������</param>
        /// <returns></returns>
        public Object get( String propertyName ) {
            EntityInfo ei = getEntityInfo();
            if (propertyName.IndexOf( "." ) < 0) {
                EntityPropertyInfo ep = ei.GetProperty( propertyName );
                if (ep == null) throw new Exception( String.Format( "property '{1}' of {0} is empty", ei.FullName, propertyName ) );
                return ep.GetValue( this );
            }
            String[] arrItems = propertyName.Split( new char[] { '.' } );
            Object result = null;
            ObjectBase<T> obj = this;
            for (int i = 0; i < arrItems.Length; i++) {
                if (i < (arrItems.Length - 1)) {
                    obj = (ObjectBase<T>)obj.get( arrItems[i] );
                }
                else {
                    result = obj.get( arrItems[i] );
                }
            }
            return result;
        }

        /// <summary>
        /// �������Ե�ֵ
        /// </summary>
        /// <param name="propertyName">��������</param>
        /// <param name="propertyValue">���Ե�ֵ</param>
        public void set( String propertyName, Object propertyValue ) {
            getEntityInfo().GetProperty( propertyName ).SetValue( this, propertyValue );
        }

        //-------------------------------------------------------------------------

        /// <summary>
        /// ��ȡ��չ�����ڲ�ĳ���ֵ
        /// </summary>
        /// <param name="propertyName">��չ��������</param>
        /// <param name="key">��չ�����ڲ�ĳ��� key</param>
        /// <returns></returns>
        public Object getExt( String propertyName, String key ) {
            Dictionary<String, Object> dic = this.getExtDic( propertyName );
            Object val = null;
            dic.TryGetValue( key, out val );
            return val;
        }

        /// <summary>
        /// ��ȡ��չ���Ա����ֵ
        /// </summary>
        /// <param name="propertyName">��չ��������</param>
        /// <returns></returns>
        public Dictionary<String, Object> getExtDic( String propertyName ) {
            Object pvalue = this.get( propertyName );
            Dictionary<String, Object> dic = new Dictionary<string, Object>();
            if (pvalue == null || strUtil.IsNullOrEmpty( pvalue.ToString() )) return dic;
            try {
                dic = JSON.ToDictionary( pvalue.ToString() );
            }
            catch {
            }
            return dic;
        }

        /// <summary>
        /// ����չ�����ڲ�ĳ�ֵ
        /// </summary>
        /// <param name="propertyName">��չ��������</param>
        /// <param name="key">��չ�����ڲ�ĳ��� key</param>
        /// <param name="val">��չ�����ڲ�ĳ��� val</param>
        public void setExt( String propertyName, String key, String val ) {

            Dictionary<String, Object> dic = this.getExtDic( propertyName );
            dic[key] = val;
            this.setExtDic( propertyName, dic );
        }

        /// <summary>
        /// ����չ���Ա���ĸ�ֵ
        /// </summary>
        /// <param name="propertyName">��չ��������</param>
        /// <param name="dic">��չ���Ե�ֵ</param>
        public void setExtDic( String propertyName, Dictionary<String, Object> dic ) {
            this.set( propertyName, JSON.DicToString( dic ) );
            this.update( propertyName );
        }

        //-------------------------------------------------------------------------

        private EntityInfo _entity;

        private EntityInfo getEntityInfo() {
            if (_entity == null) {
                _entity = Entity.GetInfo( this );
            }
            return _entity;
        }

        //private ObjectInfo _state;

        ///// <summary>
        ///// �����Ԫ���ݣ��Լ��ڶ����ѯ��ʱ����Ҫ�Ķ�����Ϣ��������
        ///// </summary>
        //[NotSave]
        //public ObjectInfo state {
        //    get {
        //        if (_state == null) {
        //            _state = new ObjectInfo( getEntityInfo() );
        //        }
        //        return _state;
        //    }
        //    set { _state = value; }
        //}

        /// <summary>
        /// ���򷽷�(����Id��С����)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int CompareTo( Object obj ) {
            // state
            //if (string.Compare( state.Order, "asc", true ) == 0) {
            //    return Id.CompareTo( ((ObjectBase<T>)obj).Id );
            //}
            return ((ObjectBase<T>)obj).Id.CompareTo(Id);
        }
                
    }
    /// <summary>
    /// ����ORM�е�����ģ�Ͷ���Ҫ�̳еĻ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="B">����ObjectOrm</typeparam>
    [Serializable]
    public class ObjectBase<T, B> : ObjectBase<T>
        where T : ObjectBase<T>
        where B : ObjectBase<T>
    {

        private string _Guid;                                   //Guid
        private DateTime _CreateTime = DateTime.Now;            //����ʱ��
        /// <summary>
        /// 
        /// </summary>
        public ObjectBase()
        {
            _Guid = System.Guid.NewGuid().ToString();
            _CreateTime = DateTools.GetNow();
        }

        /// <summary>
        /// ����Guid��ʶ�ֶ�
        /// </summary>
        [Column(Length = 50), NotNull("Sorry,����Guid����Ϊ�գ�"), Unique("Sorry,����Guid�Ѿ����ڣ�")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }

        private string _CreateTimeTicks;
        /// <summary>
        /// ���󴴽�ʱ�䣨ʱ��̶ȣ�
        /// </summary>
        [Column(Length = 25)]
        public string CreateTimeTicks
        {
            get
            {
                if (string.IsNullOrEmpty(_CreateTimeTicks))
                {
                    return _CreateTime.Ticks.ToString();
                }
                else
                {
                    return _CreateTimeTicks;
                }
            }
            set { _CreateTimeTicks = value; }
        }

        private string _CreateTimeString;
        /// <summary>
        /// ���󴴽�ʱ�䣨�����ַ�����ʽ��
        /// </summary>
        [Column(Length = 25)]
        public string CreateTimeString
        {
            get
            {
                if (string.IsNullOrEmpty(_CreateTimeString))
                {
                    return _CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return _CreateTimeString;
                }
            }
            set { _CreateTimeString=value; }
        }
    }

}
