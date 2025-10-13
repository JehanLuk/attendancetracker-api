document.addEventListener('DOMContentLoaded', function () {
    const btn = document.getElementById('notificationBtn');
    const popup = document.getElementById('notificationPopup');

    // Abre e fecha o popup ao clicar no sino
    btn.addEventListener('click', function (e) {
        e.stopPropagation();
        popup.style.display = popup.style.display === 'none' || popup.style.display === ''
            ? 'block'
            : 'none';
    });

    // Fecha o popup ao clicar fora
    document.addEventListener('click', function (e) {
        if (popup.style.display === 'block' && !popup.contains(e.target) && e.target !== btn) {
            popup.style.display = 'none';
        }
    });
});
