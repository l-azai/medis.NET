using medis.Api.Models.Videos;
using medis.Api.Interfaces.Repositories.Videos;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Reflection;

namespace medis.Api.Repositories.Videos
{
    public class VideoRepository : Repository<VideoFile>, IVideoRepository
    {
        /// <summary>
        /// Gets the videos by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public IEnumerable<VideoFile> GetByCategory(string category)
        {
            var sql = @"SELECT v.* 
                        FROM Video v
                        INNER JOIN VideoCategory vc
                            ON v.CategoryId = vc.Id
                        WHERE vc.Name = @category
                        ";

            using (var conn = GetConnection())
            {
                var videos = conn.Query<VideoFile>(sql, new { category = category });

                return videos;
            }
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IEnumerable<VideoFile> GetByName(string name)
        {
            var sql = @"SELECT v.Id, v.name
                        FROM Video v
                        WHERE v.name LIKE @videoName";

            using (var conn = GetConnection())
            {
                var videos = conn.Query<VideoFile>(sql, new { videoName = $"%{name}%" });

                return videos;
            }
        }

        /// <summary>
        /// Gets the paged results.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        public VideoSearchResults GetPagedResults(VideoSearchModel searchModel)
        {
            var sql = @"SELECT *, total_records = COUNT(*) OVER()
                        FROM Video
                        WHERE name LIKE @videoName AND categoryId = ISNULL(@categoryFilterId, categoryId)
                        ORDER BY name
                        OFFSET @skip ROWS
                        FETCH NEXT @pageSize ROWS ONLY";

            using (var conn = GetConnection())
            {
                List<VideoFile> videos = null;
                int totalRecords = 0;

                conn.Query<VideoFile, int, VideoFile>(sql, 
                    (video, recordCount) => {
                        if (videos == null) {
                            videos = new List<VideoFile>();
                        }

                        videos.Add(video);
                        totalRecords = recordCount;

                        return video;
                    }, new {
                        videoName = $"%{searchModel.searchText}%",  
                        categoryFilterId = searchModel.categoryFilterId,
                        skip = searchModel.skip,
                        pageSize = searchModel.pageSize
                    }, splitOn: "total_records");

                if (!string.IsNullOrEmpty(searchModel.sortName))
                {
                    var bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

                    videos = searchModel.sortDescending == true
                        ? videos.OrderByDescending(x => x.GetType().GetProperty(searchModel.sortName, bindingFlags).GetValue(x)).ToList()
                        : videos.OrderBy(x => x.GetType().GetProperty(searchModel.sortName, bindingFlags).GetValue(x)).ToList();
                }

                return new VideoSearchResults {
                    PagedResults = videos,
                    TotalRecords = totalRecords
                };
            }
        }
    }
}
