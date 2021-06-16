local skynet = require "skynet"

skynet.start(function()
    print("******************socket start********************")

    skynet.newservice("socket1")
    skynet.exit()
end)