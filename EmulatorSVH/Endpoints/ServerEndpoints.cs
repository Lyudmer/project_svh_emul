
using EmulatorSVH.Application.Interface;
using EmulatorSVH.ReceivSend;




namespace EmulatorSVH.Endpoints
{
    public static  class ServerEndpoints
    {
        public static IEndpointRouteBuilder MapPackagesEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("Packages");
            app.MapGet("GetPackage", GetPkgAll);
            return app;
        }
        private static async Task<IResult> GetPkgAll(EmulatorServices srvService)
        {
            await ((EmulatorServices)srvService).GetPackageList();
            return Results.Ok();
        }  
        
    }
}
