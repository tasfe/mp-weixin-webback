//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Utils\OrmUtil.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data;
namespace System.ORM {
    internal partial class OrmHelper {
        internal static void CloseDataReader( IDataReader rd ) {
            if (rd == null) return;
            if (rd.IsClosed) return;
            rd.Close();
        }
        public static Boolean IsEntityBase( Type t ) {
            return t.FullName.IndexOf( "System.ObjectBase" ) >= 0;
        }
        private static Boolean isContinue(String action, EntityPropertyInfo info, EntityInfo entityInfo)
        {
            if (info.SaveToDB == false) return true;
            if (info.IsList) return true;
            if (info.Name.Equals("Id"))
            {
                if (action.Equals("update")) return true;
                if (action.Equals("insert") && entityInfo.Parent == null) return true;
            }
            return false;
        }
        internal static void SetParameters(IDbCommand cmd, String action, IEntity obj, EntityInfo entityInfo)
        {
            for (int i = 0; i < entityInfo.SavedPropertyList.Count; i++)
            {
                EntityPropertyInfo info = entityInfo.SavedPropertyList[i];
                if (isContinue(action, info, entityInfo)) continue;
                Object paramVal = obj.get(info.Name);
                if (paramVal == null && info.DefaultAttribute != null)
                {
                    paramVal = info.DefaultAttribute.Value;
                }
                if (paramVal == null)
                {
                    setDefaultValue(cmd, info, entityInfo);
                }
                else if (info.Type.IsSubclassOf(typeof(IEntity)) || MappingClass.Instance.ClassList.Contains(info.Type.FullName))
                {
                    setEntityId(cmd, info, paramVal);
                }
                else
                {
                    paramVal = DataFactory.SetParameter(cmd, info.ColumnName, paramVal);
                    obj.set(info.Name, paramVal);
                }
            }
        }
        private static void setDefaultValue(IDbCommand cmd, EntityPropertyInfo info, EntityInfo entityInfo)
        {
            if (MappingClass.Instance.ClassList.Contains(info.Type.FullName))
            {
                DataFactory.SetParameter(cmd, info.ColumnName, -1);
            }
            else if (info.Type == typeof(DateTime))
            {
                if (entityInfo.DbType == DatabaseType.Access)
                {
                    DataFactory.SetParameter(cmd, info.ColumnName, DateTime.Now.ToString());
                }
                else
                {
                    DataFactory.SetParameter(cmd, info.ColumnName, DateTime.Now);
                }
            }
            else
            {
                DataFactory.SetParameter(cmd, info.ColumnName, "");
            }
        }
        private static void setEntityId(IDbCommand cmd, EntityPropertyInfo info, Object paramVal)
        {
            int id = ((IEntity)paramVal).Id;
            DataFactory.SetParameter(cmd, info.ColumnName, id);
        }
    }
}