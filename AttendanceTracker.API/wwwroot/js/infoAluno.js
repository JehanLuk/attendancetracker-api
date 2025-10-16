// aluno-detalhes.js
document.addEventListener('DOMContentLoaded', function() {
    const linhas = document.querySelectorAll('.linha-aluno');
    const detalhesContainer = document.getElementById('detalhes-container');
    const detalhesPainel = document.getElementById('detalhes-aluno');
    const fecharDetalhesBtn = document.getElementById('fechar-detalhes');

    // Função para mostrar detalhes do aluno
    function mostrarDetalhesAluno(linha) {
        const cells = linha.getElementsByTagName('td');
        const statusIndicador = cells[2].querySelector('.status-indicador');
        const status = statusIndicador.classList.contains('presente') ? 'Presente' : 'Ausente';
        const alunoId = linha.getAttribute('data-aluno-id');

        detalhesContainer.innerHTML = `
            <div class="detalhes-item">
                <strong>ID:</strong> <span>${alunoId}</span>
            </div>
            <div class="detalhes-item">
                <strong>Nome:</strong> <span>${cells[0].textContent}</span>
            </div>
            <div class="detalhes-item">
                <strong>Matrícula:</strong> <span>${cells[1].textContent}</span>
            </div>
            <div class="detalhes-item">
                <strong>Status:</strong> <span class="status-text ${status.toLowerCase()}">${status}</span>
            </div>
            <div class="detalhes-item">
                <strong>Entrada:</strong> <span>${cells[3].textContent}</span>
            </div>
            <div class="detalhes-item">
                <strong>Saída:</strong> <span>${cells[4].textContent}</span>
            </div>
            <div class="acoes-aluno">
                <button class="btn-editar" onclick="editarAluno(${alunoId})">Editar</button>
                <button class="btn-relatorio" onclick="gerarRelatorio(${alunoId})">Relatório</button>
            </div>
        `;

        // Mostra o painel
        detalhesPainel.classList.remove('oculto');
        detalhesPainel.classList.add('visivel');
    }

    // Função para ocultar detalhes
    function ocultarDetalhes() {
        detalhesPainel.classList.remove('visivel');
        detalhesPainel.classList.add('oculto');
        
        // Remove a seleção de todas as linhas
        removerSelecao();
    }

    // Função para remover seleção de todas as linhas
    function removerSelecao() {
        linhas.forEach(l => l.classList.remove('selecionada'));
    }

    // Adiciona evento de clique para cada linha
    linhas.forEach(linha => {
        linha.addEventListener('click', function() {
            // Remove a seleção anterior
            removerSelecao();
            
            // Adiciona seleção à linha clicada
            this.classList.add('selecionada');

            // Mostra os detalhes do aluno
            mostrarDetalhesAluno(this);
        });
    });

    // Evento para fechar o painel de detalhes
    fecharDetalhesBtn.addEventListener('click', function(e) {
        e.stopPropagation(); // Impede que o evento propague para o painel
        ocultarDetalhes();
    });

    // Evento para fechar ao clicar fora do painel (opcional)
    document.addEventListener('click', function(e) {
        // Se clicar em qualquer lugar que não seja o painel ou uma linha da tabela
        if (!e.target.closest('.detalhes-painel') && !e.target.closest('.linha-aluno')) {
            ocultarDetalhes();
        }
    });

    // Impede que cliques dentro do painel fechem ele
    detalhesPainel.addEventListener('click', function(e) {
        e.stopPropagation();
    });

    // Funções globais para os botões
    window.editarAluno = function(alunoId) {
        alert(`Editar aluno ID: ${alunoId}`);
        // Implemente a lógica de edição aqui
    };

    window.gerarRelatorio = function(alunoId) {
        alert(`Gerar relatório para aluno ID: ${alunoId}`);
        // Implemente a geração de relatório aqui
    };
});