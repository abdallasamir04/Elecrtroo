// Quick view functionality
function openQuickView(productId, name, price, description, image) {
    const quickViewProductIdElement = document.getElementById('quickViewProductId');
    const quickViewNameElement = document.getElementById('quickViewName');
    const quickViewPriceElement = document.getElementById('quickViewPrice');
    const quickViewDescriptionElement = document.getElementById('quickViewDescription');
    const quickViewImageElement = document.getElementById('quickViewImage');

    if (quickViewProductIdElement) quickViewProductIdElement.value = productId;
    if (quickViewNameElement) quickViewNameElement.textContent = name || 'Product';
    if (quickViewPriceElement) quickViewPriceElement.textContent = '$' + (price || 0).toFixed(2);
    if (quickViewDescriptionElement) quickViewDescriptionElement.textContent = description || 'No description available';
    if (quickViewImageElement) quickViewImageElement.src = image || '/images/default-placeholder.png';

    // Check if Bootstrap is available
    if (typeof bootstrap !== 'undefined') {
        const quickViewModalElement = document.getElementById('quickViewModal');
        if (quickViewModalElement) {
            const quickViewModal = new bootstrap.Modal(quickViewModalElement);
            quickViewModal.show();
        }
    } else {
        // Fallback if Bootstrap is not available
        const quickViewModalElement = document.getElementById('quickViewModal');
        if (quickViewModalElement) {
            quickViewModalElement.style.display = 'block';
        }
    }
}

function addToWishlist(productId) {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const token = tokenElement ? tokenElement.value : '';

    fetch('/Wishlist/AddToWishlist', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Requested-With': 'XMLHttpRequest',
            'RequestVerificationToken': token
        },
        body: JSON.stringify({ productId: productId })
    })
        .then(response => {
            if (response.ok) {
                alert('Product added to wishlist!');
            } else if (response.status === 401) {
                alert('You need to be logged in to add items to your wishlist.');
                window.location.href = '/Account/Login';
            } else {
                alert('Something went wrong. Please try again.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Something went wrong. Please try again.');
        });
}

function addToCompare(productId) {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const token = tokenElement ? tokenElement.value : '';

    fetch('/Compare/AddToCompare', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Requested-With': 'XMLHttpRequest',
            'RequestVerificationToken': token
        },
        body: JSON.stringify({ productId: productId })
    })
        .then(response => {
            if (response.ok) {
                alert('Product added to compare list!');
            } else {
                alert('Something went wrong. Please try again.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Something went wrong. Please try again.');
        });
}