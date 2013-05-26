//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\IInterceptor.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {
    /// <summary>
    /// �������ӿڣ��û�����ͨ���Զ������������ڲ���ǰ������ȶ�����ע���Լ��Ĵ����߼�
    /// </summary>
    public interface IInterceptor {
        void BeforInsert( IEntity entity );
        void AfterInsert( IEntity entity );
        void BeforUpdate( IEntity entity );
        void AfterUpdate( IEntity entity );
        void BeforUpdateBatch( Type t, String action, String condition );
        void AfterUpdateBatch( Type t, String action, String condition );
        void BeforDelete( IEntity entity );
        void AfterDelete( IEntity entity );
        void BeforDeleteBatch( Type t, String condition );
        void AfterDeleteBatch( Type t, String condition );
    }
}