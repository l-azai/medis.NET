using AutoMapper;
using System;
using System.Linq;

namespace medis.Api.Mappers
{
    public class AutoMapperRegistry
    {
        public static MapperConfiguration Create() {

            var profiles = typeof(AutoMapperRegistry).Assembly.GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x))
                .Select(x => (Profile)Activator.CreateInstance(x));

            var mapper = new MapperConfiguration(cfg => {

                profiles.ToList().ForEach(p => {
                    cfg.AddProfile(p);
                });
            });

            return mapper;
        }
    }
}