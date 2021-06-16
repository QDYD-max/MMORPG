local skynet = require "skynet"
local socket = require "skynet.socket"

local function echo(id)

    socket.start(id)

    while true do
        local str = socket.read(id)
        if str then
            print(id.."client say: "..str)
            socket.write(id, str)
        else
            socket.close(id)
            break
        end
    end
end

skynet.start(function ()
    print("===============socket1 start==================")

    local id = socket.listen("192.168.43.129", 8888)
    
    socket.start(id, function (id, addr)
        print("connect from " .. addr .. " " .. id)
        echo(id)
    end)
end)