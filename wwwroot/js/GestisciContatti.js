
    document.addEventListener('DOMContentLoaded', function () {
        const contattiContainer = document.getElementById('contatti-lista');
        const formContatto = document.getElementById('form-contatto');

        // Funzione per recuperare e visualizzare i contatti
        function fetchAndShowContatti() {
            fetch('http://localhost:5000/api/Contatti')
                .then(response => response.json())
                .then(data => {
                    contattiContainer.innerHTML = ''; // Svuota il contenitore dei contatti
                    data.forEach(contatto => {
                        contattiContainer.innerHTML += `
                        <div>
                            <strong>${contatto.nome} ${contatto.cognome}</strong><br>
                            Email: ${contatto.email}<br><br>
                        </div>
                    `;
                    });
                })
                .catch(error => console.error('Errore:', error));
        }

        // Visualizza i contatti all'avvio della pagina
        fetchAndShowContatti();

        // Aggiungi un listener al form per l'invio di un nuovo contatto
        formContatto.addEventListener('submit', function (event) {
            event.preventDefault(); // Evita il comportamento predefinito del form

            const nome = document.getElementById('nome').value;
            const cognome = document.getElementById('cognome').value;
            const email = document.getElementById('email').value;

            const nuovoContatto = {
                nome: nome,
                cognome: cognome,
                email: email
            };

            fetch('http://localhost:5000/api/Contatti', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(nuovoContatto)
            })
                .then(response => response.json())
                .then(data => {
                    // Aggiorna la lista dei contatti dopo l'aggiunta di uno nuovo
                    fetchAndShowContatti();
                    formContatto.reset(); // Resetta il form dopo l'aggiunta del contatto
                })
                .catch(error => console.error('Errore:', error));
        });
    });

