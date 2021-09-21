using System;
using ChronusJob.Abstractions;
using ChronusJob.Jobs.Attributes;
using Samples.AutoByDate.SqlServer.DbContexts;
using Samples.AutoByDate.SqlServer.Domain.Entities;
using ShardingCore.Core.PhysicTables;
using ShardingCore.Core.VirtualDatabase.VirtualTables;
using ShardingCore.Core.VirtualTables;
using ShardingCore.TableCreator;

namespace Samples.AutoByDate.SqlServer.Jobs
{
    /*
    * @Author: xjm
    * @Description:
    * @Date: Tuesday, 02 February 2021 17:24:17
    * @Email: 326308290@qq.com
    */
    public class AutoCreateTableByDay : IJob
    {
        /// <summary>
        /// 每天中午12点执行,启动的时候执行以下
        /// </summary>
        /// <param name="virtualTableManager"></param>
        /// <param name="tableCreator"></param>
        [JobRun(Name = "定时创建分表组件", Cron = "0 0 12 * * ?", RunOnceOnStart = true)]

        public void AutoCreateTable(IVirtualTableManager<DefaultShardingDbContext> virtualTableManager, IShardingTableCreator<DefaultShardingDbContext> tableCreator)
        {
            var virtualTable = virtualTableManager.GetVirtualTable(typeof(SysUserLogByDay));
            if (virtualTable == null)
            {
                return;
            }
            var now = DateTime.Now.Date.AddDays(1);
            var tail = virtualTable.GetVirtualRoute().ShardingKeyToTail(now);
            try
            {
                virtualTableManager.AddPhysicTable(virtualTable, new DefaultPhysicTable(virtualTable, tail)); 
                tableCreator.CreateTable("ds0",typeof(SysUserLogByDay),tail);
            }
            catch (Exception e)
            {
                //ignore
            }
        }
    }
}