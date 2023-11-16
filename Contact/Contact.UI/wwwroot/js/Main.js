const apiUrl = 'https://localhost:7246/api/contact'; 

function validateContactFields(name, mobilePhone, jobTitle, birthDate)
{
    const errors = [];

    if (!name.trim()) {
        errors.push('Please enter a name');
    }
    if (!mobilePhone.trim()) {
        errors.push('Please enter a phone number');
    }
    if (!jobTitle.trim()) {
        errors.push('Please enter a job title');
    }
    if (!birthDate.trim()) {
        errors.push('Please enter a birth date');
    }

    const englishAlphabetPattern = /^[a-zA-Z\s]+$/;
    if (!englishAlphabetPattern.test(name) || !englishAlphabetPattern.test(jobTitle)) {
        errors.push('Name and Job Title should contain only English alphabet characters');
    }

    const phonePattern = /^\+\d{12}$/;
    if (!phonePattern.test(mobilePhone)) {
        errors.push('Please enter a valid phone number (should start with "+", followed by 12 digits)');
    }

    return errors;
}

function getContacts()
{
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            displayContacts(data);
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

let isTableHeaderCreated = false;
function displayContacts(contacts)
{
    const contactsContainer = document.getElementById('contacts');
    const table = document.getElementById('contactsTable');
    let tableBody = table.querySelector('tbody');

    if (!table.querySelector('thead')) {
        const tableHeader = document.createElement('thead');
        tableHeader.innerHTML = `
            <tr>
                <th>Name</th>
                <th>Mobile Phone</th>
                <th>Job Title</th>
                <th>Birth Date</th>
                <th>Action</th>
            </tr>
        `;
        table.appendChild(tableHeader);
    }

    if (!tableBody) {
        tableBody = document.createElement('tbody');
        table.appendChild(tableBody);
    }
    tableBody.innerHTML = '';
    
    contacts.forEach(contact => {
        const dateParts = contact.birthDate.split('T')[0].split('-');
        const formattedBirthDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`;
        const contactRow = document.createElement('tr');
        contactRow.innerHTML = `
            <td>${contact.name}</td> 
            <td>${contact.mobilePhone}</td>
            <td>${contact.jobTitle}</td>
            <td>${formattedBirthDate}</td>
            <td>
                <button class="editButton" onclick="openEditContactModal(${contact.id})">Edit</button>
                <button class="deleteButton" onclick="deleteContact(${contact.id})">Delete</button>
                

            </td>
        `;
        tableBody.appendChild(contactRow);
        
        contactRow.querySelectorAll('.editButton').forEach(button => {
            button.classList.add('editButton'); 
        });

        contactRow.querySelectorAll('.deleteButton').forEach(button => {
            button.classList.add('deleteButton'); 
        });
    });
}

function openCreateContactModal()
{
    document.getElementById('createContactModal').style.display = 'block';
}

function closeCreateContactModal()
{
    document.getElementById('createContactModal').style.display = 'none';
}

function createContactFromModal()
{
    const name = document.getElementById('name').value;
    const mobilePhone = document.getElementById('mobilePhone').value;
    const jobTitle = document.getElementById('jobTitle').value;
    const birthDate = document.getElementById('birthDate').value;

    const errorMessage = document.getElementById('error-message');
    if (!errorMessage) {
        console.error('Error message element not found');
        return;
    }

    const validationErrors = validateContactFields(name, mobilePhone, jobTitle, birthDate);
    if (validationErrors.length > 0) {
        errorMessage.textContent = validationErrors.join(', ');
        return;
    }

    errorMessage.textContent = '';
    const newContact = {
        Name: name,
        MobilePhone: mobilePhone,
        JobTitle: jobTitle,
        BirthDate: birthDate
    };

    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(newContact),
    })
        .then(response => {
            if (response.status === 200) {
                getContacts();
            } else {
                return response.json();
            }
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function openEditContactModal(id)
{
    document.getElementById('editContactModal').style.display = 'block';

    const updateButton = document.getElementById('updateContactButton');

    updateButton.setAttribute('data-id', id);

    fetch(apiUrl + `/${id}`)
        .then(response => response.json())
        .then(contact => {
            document.getElementById('editName').value = contact.name;
            document.getElementById('editMobilePhone').value = contact.mobilePhone;
            document.getElementById('editJobTitle').value = contact.jobTitle;

            const birthDate = contact.birthDate.split('T')[0];
            document.getElementById('editBirthDate').value = birthDate;

            document.getElementById('editContactModal').style.display = 'block';
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function closeEditContactModal()
{
    document.getElementById('editContactModal').style.display = 'none';
}

function editContact(id)
{
    openEditContactModal(id);
}

function updateContact()
{
    const id = document.getElementById('updateContactButton').getAttribute('data-id');
    const name = document.getElementById('editName').value;
    const mobilePhone = document.getElementById('editMobilePhone').value;
    const jobTitle = document.getElementById('editJobTitle').value;
    const birthDate = document.getElementById('editBirthDate').value;

    const errorMessage = document.getElementById('edit-error-message');
    if (!errorMessage) {
        console.error('Error message element not found');
        return;
    }

    const validationErrors = validateContactFields(name, mobilePhone, jobTitle, birthDate);

    if (validationErrors.length > 0) {
        errorMessage.textContent = validationErrors.join(', ');
        return;
    }
    errorMessage.textContent = '';

    const updatedContact = {
        Name: name,
        MobilePhone: mobilePhone,
        JobTitle: jobTitle,
        BirthDate: birthDate
    };

    fetch(`${apiUrl}?id=${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedContact),
    })
        .then(response => {
            if (response.status === 200) {
                getContacts();
                closeEditContactModal();
            } else {
                return response.json();
            }
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteContact(id)
{
    fetch(apiUrl + `/${id}`, {
        method: 'DELETE',
    })
        .then(response => {
            console.log(response);
            if (response.status === 204) {
                getContacts();
            } else {
                return response.json();
            }
        })
        .then(data => {
            setTimeout(() => {
                getContacts();
            }, 500);
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

document.addEventListener('DOMContentLoaded', function ()
{
    getContacts();
});
