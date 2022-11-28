const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:44154';

const PROXY_CONFIG = [
  {
    context: [
      "/api/LectureRooms",
      "/api/Path",
      "/api/NavigationNodes",
      "/api/NavigationEdges",
      "/api/Faculties",
      "/api/Login",
      "/api/Users",
      "/swagger",

   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
