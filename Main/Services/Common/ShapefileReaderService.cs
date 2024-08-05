using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using LC.Models.Entities;

namespace TwseDataHub.Services.Common
{
    #region ShapefileReaderService
    public interface IShapefileReaderService
    {
        /// <summary>
        /// 讀取縣市界Shp
        /// </summary>
        /// <param name="shapefilePath"></param>
        /// <returns></returns>
        List<County> ReadCounty(string shapefilePath);
        /// <summary>
        /// 讀取鄉鎮縣市界Shp
        /// </summary>
        /// <param name="shapefilePath"></param>
        /// <returns></returns>
        List<Town> ReadTown(string shapefilePath);
        /// <summary>
        /// 讀取村里界Shp
        /// </summary>
        /// <param name="shapefilePath"></param>
        /// <returns></returns>
        List<Village> ReadVillage(string shapefilePath);
    }
    public class ShapefileReaderService : IShapefileReaderService
    {
        /// <inheritdoc/>
        public List<County> ReadCounty(string shapefilePath)
        {
            var factory = new GeometryFactory();
            var dataList = new List<County>();

            using (var shpReader = new ShapefileDataReader(shapefilePath, factory))
            {
                while (shpReader.Read())
                {
                    var entity = new County
                    {
                        Name = shpReader.GetValue(shpReader.GetOrdinal("COUNTYNAME"))?.ToString() ?? "",                        
                        CountyID = shpReader.GetValue(shpReader.GetOrdinal("COUNTYID"))?.ToString() ?? "",
                        CountyCode = shpReader.GetValue(shpReader.GetOrdinal("COUNTYCODE"))?.ToString() ?? "",                        
                        EngName = shpReader.GetValue(shpReader.GetOrdinal("COUNTYENG"))?.ToString() ?? "",
                        Geom = GeometryFixer.Fix(shpReader.Geometry),
                    };
                    entity.Geom.SRID = 4326;
                    dataList.Add(entity);
                }
            }

            return dataList;
        }

        /// <inheritdoc/>
        public List<Town> ReadTown(string shapefilePath)
        {
            var factory = new GeometryFactory();
            var dataList = new List<Town>();

            using (var shpReader = new ShapefileDataReader(shapefilePath, factory))
            {
                while (shpReader.Read())
                {
                    var entity = new Town
                    {
                        Name = shpReader.GetValue(shpReader.GetOrdinal("TOWNNAME"))?.ToString() ?? "",
                        CountyName = shpReader.GetValue(shpReader.GetOrdinal("COUNTYNAME"))?.ToString() ?? "",
                        CountyID = shpReader.GetValue(shpReader.GetOrdinal("COUNTYID"))?.ToString() ?? "",
                        CountyCode = shpReader.GetValue(shpReader.GetOrdinal("COUNTYCODE"))?.ToString() ?? "",
                        TownID = shpReader.GetValue(shpReader.GetOrdinal("TOWNID"))?.ToString() ?? "",
                        TownCode = shpReader.GetValue(shpReader.GetOrdinal("TOWNCODE"))?.ToString() ?? "",
                        EngName = shpReader.GetValue(shpReader.GetOrdinal("TOWNENG"))?.ToString() ?? "",
                        Geom = GeometryFixer.Fix(shpReader.Geometry),
                    };
                    entity.Geom.SRID = 4326;
                    dataList.Add(entity);
                }
            }

            return dataList;
        }

        /// <inheritdoc/>
        public List<Village> ReadVillage(string shapefilePath)
        {
            var factory = new GeometryFactory();
            var dataList = new List<Village>();

            using (var shpReader = new ShapefileDataReader(shapefilePath, factory))
            {
                while (shpReader.Read())
                {
                    var entity = new Village
                    {
                        Name = shpReader.GetValue(shpReader.GetOrdinal("VILLNAME"))?.ToString() ?? "",
                        VillageCode = shpReader.GetValue(shpReader.GetOrdinal("VILLCODE"))?.ToString() ?? "",
                        CountyName = shpReader.GetValue(shpReader.GetOrdinal("COUNTYNAME"))?.ToString() ?? "",
                        CountyID = shpReader.GetValue(shpReader.GetOrdinal("COUNTYID"))?.ToString() ?? "",
                        CountyCode = shpReader.GetValue(shpReader.GetOrdinal("COUNTYCODE"))?.ToString() ?? "",
                        TownName = shpReader.GetValue(shpReader.GetOrdinal("TOWNNAME"))?.ToString() ?? "",
                        TownID = shpReader.GetValue(shpReader.GetOrdinal("TOWNID"))?.ToString() ?? "",
                        TownCode = shpReader.GetValue(shpReader.GetOrdinal("TOWNCODE"))?.ToString() ?? "",
                        EngName = shpReader.GetValue(shpReader.GetOrdinal("VILLENG"))?.ToString() ?? "",
                        Note = shpReader.GetValue(shpReader.GetOrdinal("NOTE"))?.ToString() ?? "",
                        Geom = GeometryFixer.Fix(shpReader.Geometry),
                    };
                    entity.Geom.SRID = 4326;
                    dataList.Add(entity);
                }
            }

            return dataList;
        }        
    }

    #endregion ShapefileReaderService
}
