using ShardingCore.VirtualRoutes.Mods;
using ShardingCoreTestSqlServer3x.Domain.Entities;

namespace ShardingCoreTestSqlServer3x.Shardings
{
/*
* @Author: xjm
* @Description:
* @Date: Thursday, 14 January 2021 15:39:27
* @Email: 326308290@qq.com
*/
    public class SysUserModVirtualTableRoute : AbstractSimpleShardingModKeyStringVirtualTableRoute<SysUserMod>
    {
        public SysUserModVirtualTableRoute() : base(2,3)
        {
        }

    }
}