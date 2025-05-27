// Admin Panel JavaScript Functions

window.initializeAdmin = () => {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize popovers
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
};

window.toggleSidebar = () => {
    const sidebar = document.getElementById('sidebar');
    if (sidebar) {
        sidebar.classList.toggle('show');
    }
};

window.showToast = (title, message, type = 'info') => {
    const toastContainer = getOrCreateToastContainer();
    const toastId = 'toast-' + Date.now();
    
    const iconClass = {
        'success': 'fas fa-check-circle text-success',
        'error': 'fas fa-exclamation-circle text-danger',
        'warning': 'fas fa-exclamation-triangle text-warning',
        'info': 'fas fa-info-circle text-info'
    }[type] || 'fas fa-info-circle text-info';

    const toastHtml = `
        <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <i class="${iconClass} me-2"></i>
                <strong class="me-auto">${title}</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                ${message}
            </div>
        </div>
    `;

    toastContainer.insertAdjacentHTML('beforeend', toastHtml);
    
    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement, {
        autohide: true,
        delay: 5000
    });
    
    toast.show();
    
    // Remove toast element after it's hidden
    toastElement.addEventListener('hidden.bs.toast', () => {
        toastElement.remove();
    });
};

function getOrCreateToastContainer() {
    let container = document.querySelector('.toast-container');
    if (!container) {
        container = document.createElement('div');
        container.className = 'toast-container';
        document.body.appendChild(container);
    }
    return container;
}

window.logout = () => {
    localStorage.removeItem('authToken');
    window.location.href = '/login';
};

window.confirmDelete = (message) => {
    return confirm(message || 'Bu öğeyi silmek istediğinizden emin misiniz?');
};

// File upload helper
window.uploadFile = async (inputElement, endpoint) => {
    const file = inputElement.files[0];
    if (!file) return null;

    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData,
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('authToken')
            }
        });

        if (response.ok) {
            const result = await response.json();
            return result.url;
        } else {
            throw new Error('Upload failed');
        }
    } catch (error) {
        console.error('Upload error:', error);
        showToast('Hata', 'Dosya yükleme başarısız oldu.', 'error');
        return null;
    }
};

// Image preview
window.previewImage = (input, previewId) => {
    const file = input.files[0];
    const preview = document.getElementById(previewId);
    
    if (file && preview) {
        const reader = new FileReader();
        reader.onload = function(e) {
            preview.src = e.target.result;
            preview.style.display = 'block';
        };
        reader.readAsDataURL(file);
    }
};

// Auto-resize textarea
window.autoResizeTextarea = (element) => {
    element.style.height = 'auto';
    element.style.height = element.scrollHeight + 'px';
};

// Initialize auto-resize for all textareas
document.addEventListener('DOMContentLoaded', function() {
    const textareas = document.querySelectorAll('textarea[data-auto-resize]');
    textareas.forEach(textarea => {
        textarea.addEventListener('input', () => autoResizeTextarea(textarea));
        autoResizeTextarea(textarea); // Initial resize
    });
});

// Rich text editor initialization (if needed)
window.initializeRichTextEditor = (elementId) => {
    // This can be extended to use libraries like TinyMCE or CKEditor
    console.log('Rich text editor initialized for:', elementId);
};

// Data table helpers
window.initializeDataTable = (tableId, options = {}) => {
    // This can be extended to use libraries like DataTables
    console.log('Data table initialized for:', tableId);
};

// Chart helpers
window.initializeChart = (canvasId, chartData, chartType = 'line') => {
    // This can be extended to use libraries like Chart.js
    console.log('Chart initialized for:', canvasId);
};

// Local storage helpers
window.setLocalStorage = (key, value) => {
    localStorage.setItem(key, JSON.stringify(value));
};

window.getLocalStorage = (key) => {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : null;
};

window.removeLocalStorage = (key) => {
    localStorage.removeItem(key);
};

// Session storage helpers
window.setSessionStorage = (key, value) => {
    sessionStorage.setItem(key, JSON.stringify(value));
};

window.getSessionStorage = (key) => {
    const item = sessionStorage.getItem(key);
    return item ? JSON.parse(item) : null;
};

// Scroll to element
window.scrollToElement = (selector) => {
    const element = document.querySelector(selector);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
    }
};

// Copy to clipboard
window.copyToClipboard = async (text) => {
    try {
        await navigator.clipboard.writeText(text);
        showToast('Başarılı', 'Panoya kopyalandı.', 'success');
        return true;
    } catch (err) {
        console.error('Copy failed:', err);
        showToast('Hata', 'Panoya kopyalama başarısız oldu.', 'error');
        return false;
    }
};

// Print page
window.printPage = () => {
    window.print();
};

// Download file
window.downloadFile = (url, filename) => {
    const link = document.createElement('a');
    link.href = url;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};