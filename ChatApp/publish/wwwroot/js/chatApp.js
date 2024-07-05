(async function () {
    const userName = document.getElementById("user").value;
    const userMessage = document.getElementById("userMessage");
    const btnSend = document.getElementById("btnSend");
    const userMessages = document.getElementById("userMessages");

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    $(btnSend).click(async () => {
        const message = $(userMessage).val();

        if (!message || message === '') {
            return;
        }

        try {
            await connection.invoke("SendMessage", {
                id: generateId(),
                message,
                userName // Ensure userName is being sent correctly
            });

            $(userMessage).val('');
        } catch (err) {
            console.error(err);
        }
    });

    function generateId() {
        return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        );
    }

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
            await connection.invoke("GetChatHistory");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }

    connection.onclose(async () => {
        await start();
    });

    connection.on("ReceiveMessage", (payload) => {
        const { userName, message, formattedCreatedOn } = payload;
        const li = document.createElement("li");
        li.innerHTML = `<strong>${formattedCreatedOn}, ${userName}:</strong>${message}`;
        userMessages.prepend(li);
    });

    connection.on("ReceiveMessageHistory", (messages) => {
        userMessages.innerHTML = '';
        messages.reverse().forEach(payload => {
            const { userName, message, formattedCreatedOn } = payload;
            const li = document.createElement("li");
            li.innerHTML = `<strong>${formattedCreatedOn}, ${userName}:</strong>${message}`;
            userMessages.appendChild(li);
        });
    });

    start();
})();
