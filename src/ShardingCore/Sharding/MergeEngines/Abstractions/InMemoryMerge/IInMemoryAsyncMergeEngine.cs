﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ShardingCore.Sharding.StreamMergeEngines;

namespace ShardingCore.Sharding.MergeEngines.Abstractions.InMemoryMerge
{
    /*
    * @Author: xjm
    * @Description:
    * @Date: 2021/8/19 8:58:47
    * @Ver: 1.0
    * @Email: 326308290@qq.com
    */
    internal interface IInMemoryAsyncMergeEngine<TEntity>
    {
        Task<List<RouteQueryResult<TResult>>> ExecuteAsync<TResult>(Func<IQueryable, Task<TResult>> efQuery,
            CancellationToken cancellationToken = new CancellationToken());
    }
}
