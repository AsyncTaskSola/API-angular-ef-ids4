const PROXY_CONFIG = [
    {
        context: [
            "/api",
        ],
        target: "http://localhost:6000",//目标
        secure: true //安全
    },
    {
        context: [
            "/uploads",
        ],
        target: "https://localhost:6001",//目标
        secure: true //安全
    }

];

module.exports = PROXY_CONFIG;