document.addEventListener("DOMContentLoaded", function() {
    const sinoBtn = document.querySelector(".sino-btn");
    let container;

    sinoBtn.addEventListener("click", function() {
        if (!container) {
            container = document.createElement("div");
            container.id = "notificacoes";
            container.className = "container-notificacoes";
            container.style.display = "block"; 
            document.body.appendChild(container);
        } else {
            container.style.display = container.style.display === "none" ? "block" : "none";
        }
    });

    //conexão SignalR
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/alunoHub")
        .build();

    connection.on("CacheLimpo", function(message) {
        if (!container) {
            container = document.createElement("div");
            container.id = "notificacoes";
            container.className = "container-notificacoes";
            document.body.appendChild(container);
        }

        const div = document.createElement("div");
        div.className = "notificacao";
        div.innerText = message;

        container.appendChild(div);
        setTimeout(() => div.remove(), 5000);
    });

    connection.start().catch(err => console.error(err.toString()));
});
