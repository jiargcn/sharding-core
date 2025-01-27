﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ShardingCore.Core.ShardingConfigurations;
using ShardingCore.EFCores.OptionsExtensions;
using ShardingCore.Sharding.Abstractions;

namespace Sample.SqlServer.UnionAllMerge
{
    public static class ShardingCoreSqlServerExtension
    {
        public static void UseSqlServer<TShardingDbContext>(this ShardingConfigOptions<TShardingDbContext> option,Action<DbContextOptionsBuilder> builderConfigure=null) where  TShardingDbContext : DbContext, IShardingDbContext
        {
            option.UseShardingQuery((conStr, builder) =>
            {
                builder.UseSqlServer(conStr);
                builderConfigure?.Invoke(builder);
            });
            option.UseShardingTransaction((connection, builder) =>
            {
                builder.UseSqlServer(connection);
                builderConfigure?.Invoke(builder);
            });
        }
        private static UnionAllMergeOptionsExtension CreateOrGetUnionAllMergeExtension(this DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<UnionAllMergeOptionsExtension>() ??
               new UnionAllMergeOptionsExtension();
        public static DbContextOptionsBuilder UseUnionAllMerge<TShardingDbContext>(this DbContextOptionsBuilder optionsBuilder) where TShardingDbContext : DbContext, IShardingDbContext
        {
            var extension = optionsBuilder.CreateOrGetUnionAllMergeExtension();
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
            return optionsBuilder.ReplaceService<IQuerySqlGeneratorFactory,
                    UnionAllMergeSqlServerQuerySqlGeneratorFactory<TShardingDbContext>>()
                .ReplaceService<IQueryCompiler, UnionAllMergeQueryCompiler>(); ;
        }

    }
}
