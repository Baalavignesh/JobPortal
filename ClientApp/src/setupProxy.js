const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:29673';

const context =  [
    "/createuser",
    "/loginuser",
    "/addcategory",
    "/addsubcategory",
    "/deletecategory",
    "/deletesubcategory",
    "/getallcategory",
    "/getallsubcategory",
    "/getalljob",
    "/addjob"

];

module.exports = function(app) {
    const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
