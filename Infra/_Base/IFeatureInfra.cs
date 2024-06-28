// Licensed to the .NET Foundation under one or more agreements.

namespace LC.Infra._Base
{
    /// <summary>
    /// 介面 - 功能基礎設施
    /// </summary>
    public interface IFeatureInfra
    {
        /// <summary>
        /// 配置應用程序所需的服務
        /// </summary>
        public void ConfigureServices(WebApplicationBuilder builder);
        /// <summary>
        /// 配置應用程序的中介軟體
        /// </summary>
        public void Configure(WebApplication app);
    }

    /// <summary>
    /// 抽象 - 功能基礎設施
    /// </summary>
    public abstract class AbstractFeatureInfra : IFeatureInfra
    {
        /// <summary> 功能 </summary>
        public virtual InfraEnum Name { get; set; }
        /// <inheritdoc />
        public virtual void ConfigureServices(WebApplicationBuilder builder)
        {

        }
        /// <inheritdoc />
        public virtual void Configure(WebApplication app)
        {

        }
    }
}
