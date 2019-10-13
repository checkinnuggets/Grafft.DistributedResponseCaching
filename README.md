# Grafft.DistributedResponseCaching

An implementation of the which extends ASP.NET Core's ResponseCaching middleware to makes use of the configured IDistributedCache, hence DistributedResponseCache.

Some of the working research leading to this project...

- https://checkinnuggets.wordpress.com/2019/03/28/net-core-caching-response-caching-and-distributed-caching/

- https://github.com/checkinnuggets/DotNetCoreCachePost

Was seeing quite a bit of interest in the blog post, so have chosen to post a more complete implementation.  As I say at the end of the post - maybe I'm missing something, maybe there's a better way to do this.  Part of the reason for sharing is to find out :)
