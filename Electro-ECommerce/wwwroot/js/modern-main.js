// modern-main.js - Updated to remove jQuery dependencies and use modern JavaScript
document.addEventListener('DOMContentLoaded', function () {
    // Initialize product filters
    initProductFilters();

    // Initialize category filters
    initCategoryFilters();

    // Initialize animations
    initAnimations();
});

// Initialize product filters
function initProductFilters() {
    const priceRangeElement = document.getElementById("price-range");
    const filterCheckboxes = document.querySelectorAll(".filter-checkbox");
    const filterStatus = document.querySelectorAll(".filter-status");
    const applyPriceFilterBtn = document.getElementById("apply-price-filter");

    // Initialize price range slider if element exists and noUiSlider is available
    if (priceRangeElement && typeof noUiSlider !== "undefined") {
        noUiSlider.create(priceRangeElement, {
            start: [0, 1000],
            connect: true,
            range: {
                min: 0,
                max: 1000,
            },
            format: {
                to: (value) => Math.round(value),
                from: (value) => Number(value),
            },
        });

        // Update price display
        const priceMin = document.getElementById("price-min");
        const priceMax = document.getElementById("price-max");

        if (priceMin && priceMax) {
            priceRangeElement.noUiSlider.on("update", (values, handle) => {
                if (handle === 0) {
                    priceMin.textContent = "$" + values[0];
                } else {
                    priceMax.textContent = "$" + values[1];
                }
            });
        }
    }

    // Filter products based on selected options
    const filterProducts = () => {
        // Get selected categories
        const selectedCategories = [];
        document.querySelectorAll('.filter-checkbox:checked').forEach(checkbox => {
            selectedCategories.push(checkbox.value);
        });

        // Get price range
        const priceMinElement = document.getElementById('price-min');
        const priceMaxElement = document.getElementById('price-max');

        if (!priceMinElement || !priceMaxElement) return;

        const priceMin = parseFloat(priceMinElement.textContent.replace('$', ''));
        const priceMax = parseFloat(priceMaxElement.textContent.replace('$', ''));

        // Get selected status
        const selectedStatus = [];
        document.querySelectorAll('.filter-status:checked').forEach(checkbox => {
            selectedStatus.push(checkbox.value);
        });

        // Get CSRF token
        const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
        const token = tokenElement ? tokenElement.value : '';

        // Send AJAX request
        fetch('/Home/FilterProducts', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            },
            body: JSON.stringify({
                categories: selectedCategories,
                minPrice: priceMin,
                maxPrice: priceMax,
                status: selectedStatus
            })
        })
            .then(response => response.json())
            .then(data => {
                updateProductDisplay(data);
            })
            .catch(error => console.error('Error:', error));
    };

    // Add event listeners to filter checkboxes
    if (filterCheckboxes) {
        filterCheckboxes.forEach((checkbox) => {
            checkbox.addEventListener("change", filterProducts);
        });
    }

    // Add event listeners to status filters
    if (filterStatus) {
        filterStatus.forEach((status) => {
            status.addEventListener("change", filterProducts);
        });
    }

    // Apply price filter
    if (applyPriceFilterBtn) {
        applyPriceFilterBtn.addEventListener("click", filterProducts);
    }
}

// Initialize category filters
function initCategoryFilters() {
    document.querySelectorAll(".section-tab-nav a").forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            var category = this.textContent.trim();

            document.querySelectorAll(".section-tab-nav li").forEach(function (item) {
                item.classList.remove("active");
            });
            this.parentElement.classList.add("active");

            if (category === "ALL") {
                document.querySelectorAll(".product-item").forEach(function (item) {
                    item.style.display = "block";
                });
            } else {
                document.querySelectorAll(".product-item").forEach(function (item) {
                    if (item.getAttribute("data-category") === category) {
                        item.style.display = "block";
                    } else {
                        item.style.display = "none";
                    }
                });
            }
        });
    });
}

// Initialize animations
function initAnimations() {
    function animateOnScroll() {
        document.querySelectorAll('.animate-on-scroll').forEach(function (element) {
            var position = element.getBoundingClientRect().top;
            var windowHeight = window.innerHeight;
            var scrollPosition = window.scrollY || document.documentElement.scrollTop;

            if (scrollPosition + windowHeight > position + 100) {
                element.classList.add('animate-fade-in');
            }
        });
    }

    // Run on page load
    animateOnScroll();

    // Run on scroll
    window.addEventListener('scroll', animateOnScroll);
}

// Update product display
function updateProductDisplay(products) {
    const productsContainer = document.getElementById('filtered-products');
    if (!productsContainer) return;

    productsContainer.innerHTML = '';

    if (products.length === 0) {
        productsContainer.innerHTML = '<div class="col-12 text-center py-5"><h3>No products found</h3></div>';
        return;
    }

    products.forEach(product => {
        const productHtml = `
            <div class="col-md-4 col-lg-3 product-item" data-category="${product.categoryName || ''}">
                <div class="product">
                    <div class="product-img">
                        <img src="${product.imagePath || '/images/default-placeholder.png'}" alt="${product.name || 'Product'}" />
                        ${product.isOnSale ? '<div class="product-label"><span class="sale">SALE</span></div>' : ''}
                        ${product.isNew ? '<div class="product-label"><span class="new">NEW</span></div>' : ''}
                    </div>
                    <div class="product-body">
                        <p class="product-category">${product.categoryName || 'Uncategorized'}</p>
                        <h3 class="product-name"><a href="/Home/Details/${product.productId}">${product.name || 'Product'}</a></h3>
                        <h4 class="product-price">$${product.price.toFixed(2)} 
                            ${product.originalPrice ? `<del class="product-old-price">$${product.originalPrice.toFixed(2)}</del>` : ''}
                        </h4>
                        <div class="product-rating">
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star-half-o"></i>
                        </div>
                        <div class="product-btns">
                            <button class="add-to-wishlist" onclick="addToWishlist(${product.productId})">
                                <i class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span>
                            </button>
                            <button class="add-to-compare" onclick="addToCompare(${product.productId})">
                                <i class="fa fa-exchange"></i><span class="tooltipp">Compare</span>
                            </button>
                            <button class="quick-view" onclick="openQuickView(${product.productId}, '${product.name || ''}', ${product.price}, '${product.description || ''}', '${product.imagePath || '/images/default-placeholder.png'}')">
                                <i class="fa fa-eye"></i><span class="tooltipp">Quick view</span>
                            </button>
                        </div>
                    </div>
                    <div class="add-to-cart">
                        <form action="/ShoppingCart/AddToCart" method="post">
                            <input type="hidden" name="ProductId" value="${product.productId}" />
                            <button type="submit" class="add-to-cart-btn">
                                <i class="fa fa-shopping-cart"></i> Add to cart
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        `;
        productsContainer.innerHTML += productHtml;
    });
}

// Global functions for product interactions
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
                window.location.href = '/Account/Login';
            } else {
                throw new Error('Failed to add to wishlist');
            }
        })
        .catch(error => console.error('Error:', error));
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
                throw new Error('Failed to add to compare list');
            }
        })
        .catch(error => console.error('Error:', error));
}

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