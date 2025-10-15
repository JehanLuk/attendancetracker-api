document.addEventListener('DOMContentLoaded', function () {
    const notificationBadge = document.getElementById('notificationBadge');
    const ul = document.getElementById('notificationList');
    const popup = document.getElementById('notificationPopup');
    const btn = document.getElementById('notificationBtn');

    if (!notificationBadge || !ul || !popup || !btn) return;

    let notificationCount = parseInt(localStorage.getItem('notificationCount')) || 0;

    function updateNotificationCount(newCount) {
        notificationCount = newCount;
        notificationBadge.textContent = notificationCount;
        notificationBadge.style.display = notificationCount > 0 ? 'block' : 'none';
        localStorage.setItem('notificationCount', notificationCount);
    }

    function saveNotification(text) {
        let saved = JSON.parse(localStorage.getItem('notifications')) || [];
        saved.push({ text: text, time: new Date().toLocaleString() });
        localStorage.setItem('notifications', JSON.stringify(saved));
    }

    function loadSavedNotifications() {
        const saved = JSON.parse(localStorage.getItem('notifications')) || [];
        ul.innerHTML = '';
        if (saved.length === 0) {
            const li = document.createElement('li');
            li.textContent = 'Nenhuma notificação';
            li.classList.add('empty');
            ul.appendChild(li);
        } else {
            saved.forEach(entry => {
                const li = document.createElement('li');
                li.textContent = `[${entry.time}] ${entry.text}`;
                ul.appendChild(li);
            });
        }
    }

    function clearNotifications() {
        localStorage.removeItem('notifications');
        updateNotificationCount(0);
        loadSavedNotifications();
    }

    let clearBtn = document.getElementById('clearNotificationsBtn');
    if (!clearBtn) {
        clearBtn = document.createElement('button');
        clearBtn.id = 'clearNotificationsBtn';
        clearBtn.textContent = 'Limpar todas';
        clearBtn.addEventListener('click', clearNotifications);
        popup.appendChild(clearBtn);
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/alunoHub")
        .build();

    connection.on("CacheLimpo", function (mensagem) {
        saveNotification(mensagem);
        loadSavedNotifications();
        updateNotificationCount(notificationCount + 1);
    });

    connection.start().catch(err => console.error(err.toString()));

    loadSavedNotifications();
    updateNotificationCount(notificationCount);

    btn.addEventListener('click', function (e) {
        e.stopPropagation();
        const isHidden = getComputedStyle(popup).display === 'none';
        popup.style.display = isHidden ? 'block' : 'none';
        if (isHidden) updateNotificationCount(0);
    });

    document.addEventListener('click', function (e) {
        if (getComputedStyle(popup).display === 'block' && !popup.contains(e.target) && e.target !== btn) {
            popup.style.display = 'none';
        }
    });
});
