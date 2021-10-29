using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ApiBase {

    public static class HttpRequestExtensions {
        public static string[] GetUserLanguages(this HttpRequest request) {
            return request.GetTypedHeaders()
                .AcceptLanguage
                ?.OrderByDescending(x => x.Quality ?? 1)
                .Select(x => x.Value.ToString())
                .ToArray() ?? Array.Empty<string>();
        }

        public static string GetBaseUrl(this HttpRequest request) {
            return $"{request.Scheme}://{request.Host.ToString().TrimEnd('/')}";
        }
    }
}