package.cpath = "luaclib/?.so"
package.path = "lualib/?.lua"

local socket = require "client.socket"

local fd = assert(socket.connect("192.168.43.129", 8888))

function qwe()
    
end

socket.send(fd, "Hello World")
while true do
    local str = socket.recv(fd)
    if str ~= nil and str ~= "" then 
        print("server say: " .. str) 
    end

    local readstr = socket.readstdin()
    if readstr then
        if readstr == "quit" then
            socket.close(fd)
            break
        else
            socket.send(fd, readstr)
        end
    else
        socket.usleep(100)
    end
end
