document.addEventListener('DOMContentLoaded', function () {
    const notificationBadge = document.getElementById('notificationBadge');
    const ul = document.getElementById('notificationList');
    const popup = document.getElementById('notificationPopup');
    const btn = document.getElementById('notificationBtn');

    let notificationCount = parseInt(localStorage.getItem('notificationCount')) || 0;

    function updateNotificationCount(newCount) {
        notificationCount = newCount;
        notificationBadge.textContent = notificationCount;
        notificationBadge.style.display = notificationCount > 0 ? 'block' : 'none';
        localStorage.setItem('notificationCount', notificationCount);
    }

    function saveNotification(mensagem) {
        let saved = JSON.parse(localStorage.getItem('notifications')) || [];
        saved.push(mensagem);
        localStorage.setItem('notifications', JSON.stringify(saved));
    }

    function loadSavedNotifications() {
        const saved = JSON.parse(localStorage.getItem('notifications')) || [];
        ul.innerHTML = '';
        saved.forEach(msg => {
            const li = document.createElement('li');
            li.textContent = msg;
            ul.appendChild(li);
        });
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/alunoHub")
        .build();

    connection.start().catch(err => console.error(err.toString()));

    connection.on("CacheLimpo", function (mensagem) {
        saveNotification(mensagem);
        loadSavedNotifications();
        updateNotificationCount(notificationCount + 1);
    });

    loadSavedNotifications();
    updateNotificationCount(notificationCount);

    btn.addEventListener('click', () => {
        const isHidden = popup.style.display === 'none' || popup.style.display === '';

        if (isHidden) {
            popup.style.display = 'block';

            updateNotificationCount(0);
        } else {
            popup.style.display = 'none';
        }
    });

    document.addEventListener('click', (e) => {
        if (popup.style.display === 'block' && !popup.contains(e.target) && e.target !== btn) {
            popup.style.display = 'none';
        }
    });
});
