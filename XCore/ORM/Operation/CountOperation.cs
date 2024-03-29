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
using System.Data;
using System;
using System.Data;
using System.Log;

namespace System.ORM.Operation {

    internal class CountOperation {

        private static readonly ILog logger = LogManager.GetLogger( typeof( CountOperation ) );

        public static int Count( Type t ) {
            return Count( t, "" );
        }

        public static int Count( Type t, String condition ) {
            return Count( condition, Entity.GetInfo( t ) );
        }

        private static int Count( String condition, EntityInfo entityInfo ) {
            String countSql;
            int result = 0;
            SqlBuilder builder = new SqlBuilder( entityInfo );
            if (strUtil.IsNullOrEmpty( condition )) {
                countSql = String.Format( "select count(*) from {0}", entityInfo.TableName );
            }
            else {
                countSql = builder.GetCountSql( condition );
            }
            logger.Info( LoggerUtil.SqlPrefix+"[Count(String condition) Sql]:" + countSql );
            IDbCommand command = DataFactory.GetCommand( countSql, DbContext.getConnection( entityInfo ) );
            try {
                result = cvt.ToInt( command.ExecuteScalar() );
            }
            catch (Exception exception) {
                logger.Error( exception.Message );
                throw exception;
            }

            return result;
        }


    }
}

