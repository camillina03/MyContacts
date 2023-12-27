async function fetchContacts() {
    try {
        const response = await fetch('/api/Contatti/GetContatti'); // Sostituisci con il tuo URL dell'API
        if (!response.ok) {
            throw new Error('Errore nella richiesta.');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Si è verificato un errore:', error);
        return []; // Ritorna un array vuoto in caso di errore
    }
}

// Funzione per popolare la tabella con i dati ottenuti dalla API
async function renderContactsFromApi() {
    const tableBody = document.getElementById('contactTable');
    const contatti = await fetchContacts();

    contatti.forEach(contact => {
        const row = document.createElement('tr');
        const birthDate = new Date(contact.dataDiNascita);
        row.innerHTML = `
            <td>${contact.nome}</td>
            <td>${contact.cognome}</td>
            <td>${contact.mail}</td>
            <td>${contact.telefono}</td>
            <td>${contact.città}</td>
            <td>${contact.sesso == 0 ? "Femmina" : "Maschio"}</td>
            <td>${contact.dataDiNascita.toString() != "0001-01-01T00:00:00" ? birthDate.toLocaleDateString() : ""}</td>
         `;

        //listener per fare in modo che le righe della tabella siano cliccabili
        row.addEventListener('click', () => {
            const selectedRow = document.querySelector('.selected');
            if (selectedRow) {
                selectedRow.classList.remove('selected');
                selectedRow.querySelector('.buttons').remove();
            }

            row.classList.add('selected');
            const buttonsContainer = document.createElement('div');
            buttonsContainer.classList.add('buttons');
            buttonsContainer.classList.add('buttons-section');
            buttonsContainer.innerHTML = `
                <button onclick="showDetails(${contact.mail})">Dettagli</button>
                <button onclick="updateContact(${contact.mail})">Aggiorna</button>
                <button onclick="deleteContact(${contact.mail})">Elimina</button>
            `;
            row.appendChild(buttonsContainer);
        });

        tableBody.appendChild(row);
    });
}

// Chiama la funzione per popolare la tabella al caricamento della pagina

async function showDetails(mail) {
    try {
        const response = await fetch(`/api/contatti/GetContatto${mail}`);
        if (!response.ok) {
            throw new Error('Errore nella richiesta.');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Si è verificato un errore durante il recupero dei dettagli del contatto:', error);
    }
}
window.onload = function () {
    renderContactsFromApi();
};