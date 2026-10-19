import { r as e } from "./rolldown-runtime-hePW80VL.js";
import { n as t, t as n } from "./vendor-ui-Be8JR3ju.js";
import {
    _ as r,
    d as i,
    h as a,
    l as o,
    o as s,
    s as c,
    v as l,
    y as u,
} from "./main-BomV8aNu.js";
import { t as d } from "./search-component-7FwxNuMO.js";
var f = e(n(), 1);
document.addEventListener(`alpine:init`, () => {
    (t.data(`productTable`, () => {
        let e = {};
        return {
            products: [],
            filteredProducts: [],
            selectedProducts: [],
            currentPage: 1,
            itemsPerPage: 10,
            searchQuery: ``,
            categoryFilter: ``,
            stockFilter: ``,
            sortField: `name`,
            sortDirection: `asc`,
            isLoading: !1,
            chartsInitialized: !1,
            stats: { total: 0, inStock: 0, lowStock: 0, totalValue: 0 },
            categoryStats: [],
            async init() {
                await this.loadProducts();
                this.filterProducts(),
                this.calculateStats(),
                // this.loadSampleData(),
                setTimeout(() => {
                    this.initCharts();
                }, 500);
            },
            loadSampleData() {
                this.products = [
                    {
                        id: 1,
                        name: `iPhone 14 Pro`,
                        sku: `IPHONE14-PRO-128`,
                        category: `electronics`,
                        price: 999.99,
                        stock: 45,
                        status: `published`,
                        created: `2024-01-15`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Latest iPhone with advanced camera system`,
                    },
                    {
                        id: 2,
                        name: `MacBook Air M2`,
                        sku: `MBA-M2-256`,
                        category: `electronics`,
                        price: 1199.99,
                        stock: 23,
                        status: `published`,
                        created: `2024-01-20`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Lightweight laptop with M2 chip`,
                    },
                    {
                        id: 3,
                        name: `Cotton T-Shirt`,
                        sku: `TSHIRT-COT-M`,
                        category: `clothing`,
                        price: 24.99,
                        stock: 156,
                        status: `published`,
                        created: `2024-02-01`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `100% organic cotton t-shirt`,
                    },
                    {
                        id: 4,
                        name: `JavaScript Guide`,
                        sku: `BOOK-JS-2024`,
                        category: `books`,
                        price: 39.99,
                        stock: 8,
                        status: `published`,
                        created: `2024-02-10`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Complete JavaScript programming guide`,
                    },
                    {
                        id: 5,
                        name: `Garden Tool Set`,
                        sku: `GARDEN-TOOLS-SET`,
                        category: `home`,
                        price: 89.99,
                        stock: 0,
                        status: `published`,
                        created: `2024-02-15`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Professional garden tool kit`,
                    },
                    {
                        id: 6,
                        name: `Wireless Headphones`,
                        sku: `HEADPHONES-WL-BT`,
                        category: `electronics`,
                        price: 149.99,
                        stock: 67,
                        status: `published`,
                        created: `2024-02-20`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Noise-cancelling wireless headphones`,
                    },
                    {
                        id: 7,
                        name: `Denim Jeans`,
                        sku: `JEANS-DENIM-32`,
                        category: `clothing`,
                        price: 79.99,
                        stock: 34,
                        status: `draft`,
                        created: `2024-02-25`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Classic fit denim jeans`,
                    },
                    {
                        id: 8,
                        name: `Python Cookbook`,
                        sku: `BOOK-PY-COOK`,
                        category: `books`,
                        price: 44.99,
                        stock: 15,
                        status: `published`,
                        created: `2024-03-01`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Advanced Python programming techniques`,
                    },
                    {
                        id: 9,
                        name: `Smart Home Hub`,
                        sku: `SMARTHUB-V2`,
                        category: `electronics`,
                        price: 199.99,
                        stock: 12,
                        status: `published`,
                        created: `2024-03-05`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Central hub for smart home devices`,
                    },
                    {
                        id: 10,
                        name: `Kitchen Knife Set`,
                        sku: `KITCHEN-KNIVES-PRO`,
                        category: `home`,
                        price: 129.99,
                        stock: 28,
                        status: `pending`,
                        created: `2024-03-10`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Professional chef knife collection`,
                    },
                    {
                        id: 11,
                        name: `Samsung Galaxy S24`,
                        sku: `GALAXY-S24-256`,
                        category: `electronics`,
                        price: 899.99,
                        stock: 67,
                        status: `published`,
                        created: `2024-03-12`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Latest Samsung flagship smartphone`,
                    },
                    {
                        id: 12,
                        name: `Yoga Mat Premium`,
                        sku: `YOGA-MAT-PREM`,
                        category: `home`,
                        price: 49.99,
                        stock: 156,
                        status: `published`,
                        created: `2024-03-14`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Non-slip premium yoga mat`,
                    },
                    {
                        id: 13,
                        name: `React Handbook`,
                        sku: `BOOK-REACT-2024`,
                        category: `books`,
                        price: 54.99,
                        stock: 23,
                        status: `published`,
                        created: `2024-03-16`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Complete React development guide`,
                    },
                    {
                        id: 14,
                        name: `Winter Jacket`,
                        sku: `JACKET-WINTER-L`,
                        category: `clothing`,
                        price: 189.99,
                        stock: 12,
                        status: `published`,
                        created: `2024-03-18`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Waterproof winter jacket`,
                    },
                    {
                        id: 15,
                        name: `Gaming Mouse RGB`,
                        sku: `MOUSE-GAMING-RGB`,
                        category: `electronics`,
                        price: 79.99,
                        stock: 89,
                        status: `published`,
                        created: `2024-03-20`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `High-precision gaming mouse`,
                    },
                    {
                        id: 16,
                        name: `Coffee Maker Deluxe`,
                        sku: `COFFEE-MAKER-DLX`,
                        category: `home`,
                        price: 249.99,
                        stock: 34,
                        status: `published`,
                        created: `2024-03-22`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Programmable drip coffee maker`,
                    },
                    {
                        id: 17,
                        name: `Node.js Complete Guide`,
                        sku: `BOOK-NODEJS-COMP`,
                        category: `books`,
                        price: 59.99,
                        stock: 18,
                        status: `published`,
                        created: `2024-03-24`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Master Node.js development`,
                    },
                    {
                        id: 18,
                        name: `Running Shoes`,
                        sku: `SHOES-RUN-42`,
                        category: `clothing`,
                        price: 129.99,
                        stock: 0,
                        status: `published`,
                        created: `2024-03-26`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Lightweight running shoes`,
                    },
                    {
                        id: 19,
                        name: `Tablet Pro 12.9"`,
                        sku: `TABLET-PRO-129`,
                        category: `electronics`,
                        price: 1099.99,
                        stock: 15,
                        status: `published`,
                        created: `2024-03-28`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Professional tablet with stylus`,
                    },
                    {
                        id: 20,
                        name: `Garden Planter Set`,
                        sku: `PLANTER-SET-3PC`,
                        category: `home`,
                        price: 89.99,
                        stock: 45,
                        status: `published`,
                        created: `2024-03-30`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Set of 3 ceramic planters`,
                    },
                    {
                        id: 21,
                        name: `Docker Deep Dive`,
                        sku: `BOOK-DOCKER-DD`,
                        category: `books`,
                        price: 49.99,
                        stock: 31,
                        status: `published`,
                        created: `2024-04-01`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Container technology mastery`,
                    },
                    {
                        id: 22,
                        name: `Casual Polo Shirt`,
                        sku: `POLO-CASUAL-M`,
                        category: `clothing`,
                        price: 39.99,
                        stock: 78,
                        status: `published`,
                        created: `2024-04-03`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Premium cotton polo shirt`,
                    },
                    {
                        id: 23,
                        name: `Mechanical Keyboard`,
                        sku: `KEYBOARD-MECH-TKL`,
                        category: `electronics`,
                        price: 159.99,
                        stock: 24,
                        status: `published`,
                        created: `2024-04-05`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Tenkeyless mechanical keyboard`,
                    },
                    {
                        id: 24,
                        name: `Desk Organizer`,
                        sku: `DESK-ORG-BAMBOO`,
                        category: `home`,
                        price: 34.99,
                        stock: 62,
                        status: `published`,
                        created: `2024-04-07`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Bamboo desk organizer`,
                    },
                    {
                        id: 25,
                        name: `AI & Machine Learning`,
                        sku: `BOOK-AI-ML-2024`,
                        category: `books`,
                        price: 79.99,
                        stock: 14,
                        status: `published`,
                        created: `2024-04-09`,
                        image: `./assets/images/product-placeholder.svg`,
                        description: `Introduction to AI and ML`,
                    },
                ];
            },

            async loadProducts() {
                try { 

                    const response = await fetch('/api/Products', {
                        method: 'GET',
                        headers: {
                            'Accept': 'application/json'
                        }
                    });

                    if (!response.ok) {
                        throw new Error(`Lỗi tải sản phẩm: ${response.status}`);
                    }

                    const result = await response.json();

                    // API ShopEcom trả danh sách sản phẩm trong thuộc tính items.
                    const items = result.items ?? [];

                    this.products = items.map(product => ({
                        id: product.id,
                        name: product.name,
                        sku: `PRODUCT-${product.id}`,
                        category: product.categoryName ?? `category-${product.categoryId}`,
                        price: Number(product.price) || 0,
                        stock: Number(product.stock) || 0,
                        status: 'published',
                        created: '',
                        image: product.images?.[0] ?? './assets/images/product-placeholder.svg',
                        description: product.description ?? ''
                    }));
                } catch (error) {
                    console.error('Không thể tải danh sách sản phẩm:', error);
                    this.products = [];
                }
            },


            calculateStats() {
                ((this.stats.total = this.products.length),
                    (this.stats.inStock = this.products.filter(
                        (e) => e.stock > 20,
                    ).length),
                    (this.stats.lowStock = this.products.filter(
                        (e) => e.stock > 0 && e.stock <= 20,
                    ).length),
                    (this.stats.totalValue = this.products.reduce(
                        (e, t) => e + t.price * t.stock,
                        0,
                    )));
                let e = {};
                (this.products.forEach((t) => {
                    e[t.category] = (e[t.category] || 0) + 1;
                }),
                    (this.categoryStats = Object.entries(e).map(([e, t]) => ({
                        name: e.charAt(0).toUpperCase() + e.slice(1),
                        count: t,
                        percentage: Math.round((t / this.products.length) * 100),
                        color: this.getCategoryColor(e),
                    }))));
            },
            getCategoryColor(e) {
                return (
                    {
                        electronics: o(4)[0],
                        clothing: o(4)[1],
                        books: o(4)[2],
                        home: o(4)[3],
                    }[e] || s.neutral
                );
            },
            filterProducts() {
                ((this.filteredProducts = this.products.filter((e) => {
                    let t =
                        !this.searchQuery ||
                        e.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
                        e.sku.toLowerCase().includes(this.searchQuery.toLowerCase()),
                        n = !this.categoryFilter || e.category === this.categoryFilter,
                        r =
                            !this.stockFilter ||
                            (this.stockFilter === `in-stock` && e.stock > 20) ||
                            (this.stockFilter === `low-stock` &&
                                e.stock > 0 &&
                                e.stock <= 20) ||
                            (this.stockFilter === `out-of-stock` && e.stock === 0);
                    return t && n && r;
                })),
                    this.sortProducts(),
                    (this.currentPage = 1));
            },
            sortProducts() {
                this.filteredProducts.sort((e, t) => {
                    let n = e[this.sortField],
                        r = t[this.sortField];
                    return (
                        this.sortField === `price` || this.sortField === `stock`
                            ? ((n = parseFloat(n)), (r = parseFloat(r)))
                            : this.sortField === `created`
                                ? ((n = new Date(n)), (r = new Date(r)))
                                : ((n = n.toString().toLowerCase()),
                                    (r = r.toString().toLowerCase())),
                        this.sortDirection === `asc`
                            ? n < r
                                ? -1
                                : +(n > r)
                            : n > r
                                ? -1
                                : +(n < r)
                    );
                });
            },
            sortBy(e) {
                (this.sortField === e
                    ? (this.sortDirection = this.sortDirection === `asc` ? `desc` : `asc`)
                    : ((this.sortField = e), (this.sortDirection = `asc`)),
                    this.filterProducts());
            },
            toggleAll(e) {
                this.selectedProducts = e
                    ? this.paginatedProducts.map((e) => e.id)
                    : [];
            },
            bulkAction(e) {
                if (this.selectedProducts.length === 0) return;
                let t = this.products.filter((e) =>
                    this.selectedProducts.includes(e.id),
                );
                switch (e) {
                    case `publish`:
                        (t.forEach((e) => {
                            e.status = `published`;
                        }),
                            this.showNotification(
                                `Products published successfully!`,
                                `success`,
                            ));
                        break;
                    case `unpublish`:
                        (t.forEach((e) => {
                            e.status = `draft`;
                        }),
                            this.showNotification(
                                `Products unpublished successfully!`,
                                `info`,
                            ));
                        break;
                    case `delete`:
                        confirm(
                            `Are you sure you want to delete ${this.selectedProducts.length} product(s)?`,
                        ) &&
                            ((this.products = this.products.filter(
                                (e) => !this.selectedProducts.includes(e.id),
                            )),
                                this.filterProducts(),
                                this.calculateStats(),
                                this.showNotification(
                                    `Products deleted successfully!`,
                                    `success`,
                                ));
                }
                this.selectedProducts = [];
            },
            editProduct(e) {
                (console.log(`Edit product:`, e),
                    this.showNotification(`Edit functionality would open here`, `info`));
            },
            viewProduct(e) {
                (console.log(`View product:`, e),
                    this.showNotification(`Product details would open here`, `info`));
            },
            duplicateProduct(e) {
                let t = {
                    ...e,
                    id: Math.max(...this.products.map((e) => e.id)) + 1,
                    name: e.name + ` (Copy)`,
                    sku: e.sku + `-COPY`,
                    status: `draft`,
                    created: new Date().toISOString().split(`T`)[0],
                };
                (this.products.unshift(t),
                    this.filterProducts(),
                    this.calculateStats(),
                    this.showNotification(`Product duplicated successfully!`, `success`));
            },
            deleteProduct(e) {
                confirm(`Are you sure you want to delete "${e.name}"?`) &&
                    ((this.products = this.products.filter((t) => t.id !== e.id)),
                        this.filterProducts(),
                        this.calculateStats(),
                        this.showNotification(`Product deleted successfully!`, `success`));
            },
            exportProducts() {
                let e =
                    `data:text/csv;charset=utf-8,Name,SKU,Category,Price,Stock,Status,Created
` +
                    this.filteredProducts.map(
                        (e) =>
                            `"${e.name}","${e.sku}","${e.category}","${e.price}","${e.stock}","${e.status}","${e.created}"`,
                    ).join(`
`),
                    t = encodeURI(e),
                    n = document.createElement(`a`);
                (n.setAttribute(`href`, t),
                    n.setAttribute(`download`, `products.csv`),
                    document.body.appendChild(n),
                    n.click(),
                    document.body.removeChild(n),
                    this.showNotification(`Products exported successfully!`, `success`));
            },
            showNotification(e, t = `info`) {
                f.default === void 0
                    ? alert(e)
                    : f.default.fire({
                        title: e,
                        icon:
                            t === `success` ? `success` : t === `error` ? `error` : `info`,
                        toast: !0,
                        position: `top-end`,
                        showConfirmButton: !1,
                        timer: 3e3,
                    });
            },
            initCharts() {
                this.chartsInitialized ||
                    (this.initSalesChart(),
                        this.initCategoryChart(),
                        this.initPeriodSelector(),
                        (this.chartsInitialized = !0));
            },
            buildDayLabels(e) {
                let t = new Date();
                return Array.from({ length: e }, (n, r) => {
                    let i = new Date(t);
                    return (
                        i.setDate(i.getDate() - (e - 1 - r)),
                        e <= 7
                            ? i.toLocaleDateString(`en-US`, { weekday: `short` })
                            : i.toLocaleDateString(`en-US`, {
                                month: `short`,
                                day: `numeric`,
                            })
                    );
                });
            },
            generateSalesData(e) {
                return Array.from(
                    { length: e },
                    () => Math.floor(Math.random() * 60) + 50,
                );
            },
            initPeriodSelector() {
                let t = { sales7d: 7, sales30d: 30, sales90d: 90 };
                document.querySelectorAll(`input[name="salesPeriod"]`).forEach((n) => {
                    n.addEventListener(`change`, (n) => {
                        let r = t[n.target.id],
                            i = e.sales;
                        r &&
                            i &&
                            ((i.data.labels = this.buildDayLabels(r)),
                                (i.data.datasets[0].data = this.generateSalesData(r)),
                                i.update());
                    });
                });
            },
            initSalesChart() {
                let t = document.getElementById(`salesChart`);
                if (!t) {
                    console.warn(`Sales chart element not found`);
                    return;
                }
                try {
                    e.sales = l(
                        t,
                        {
                            type: `line`,
                            data: {
                                labels: [`Mon`, `Tue`, `Wed`, `Thu`, `Fri`, `Sat`, `Sun`],
                                datasets: [
                                    {
                                        label: `Sales`,
                                        data: [65, 78, 85, 92, 88, 95, 102],
                                        borderColor: () => c(),
                                        backgroundColor: a(() => c(), 0.5, 0.1),
                                        pointBackgroundColor: () => c(),
                                        fill: !0,
                                    },
                                ],
                            },
                            options: {
                                interaction: { mode: `index`, intersect: !1 },
                                scales: r({ title: `Sales ($1000s)`, beginAtZero: !1 }),
                                plugins: {
                                    legend: { position: `bottom` },
                                    tooltip: {
                                        callbacks: { label: (e) => `Sales: $${e.parsed.y}k` },
                                    },
                                },
                            },
                        },
                        { height: 300 },
                    );
                } catch (e) {
                    console.error(`Error rendering sales chart:`, e);
                }
            },
            initCategoryChart() {
                let t = document.getElementById(`categoryChart`);
                if (!t) {
                    console.warn(`Category chart element not found`);
                    return;
                }
                try {
                    let n = this.categoryStats;
                    e.category = l(
                        t,
                        {
                            type: `doughnut`,
                            data: {
                                labels: n.map((e) => e.name),
                                datasets: [
                                    {
                                        label: `Products`,
                                        data: n.map((e) => e.count),
                                        backgroundColor: (e) =>
                                            this.getCategoryColor(n[e.dataIndex]?.name.toLowerCase()),
                                    },
                                ],
                            },
                            options: {
                                cutout: `70%`,
                                plugins: {
                                    legend: { display: !1 },
                                    tooltip: {
                                        callbacks: {
                                            label: (e) => `${e.label}: ${e.parsed} products`,
                                        },
                                    },
                                    sliceLabels: { color: i() },
                                },
                            },
                            plugins: [u],
                        },
                        { height: 200 },
                    );
                } catch (e) {
                    console.error(`Error rendering category chart:`, e);
                }
            },
            get paginatedProducts() {
                let e = (this.currentPage - 1) * this.itemsPerPage,
                    t = e + this.itemsPerPage;
                return this.filteredProducts.slice(e, t);
            },
            get totalPages() {
                return Math.ceil(this.filteredProducts.length / this.itemsPerPage);
            },
            get visiblePages() {
                if (this.totalPages <= 1) return [1];
                let e = [];
                if ((e.push(1), this.totalPages <= 7))
                    for (let t = 2; t <= this.totalPages; t++) e.push(t);
                else if (this.currentPage <= 4) {
                    for (let t = 2; t <= 5; t++) e.push(t);
                    (e.push(`...`), e.push(this.totalPages));
                } else if (this.currentPage >= this.totalPages - 3) {
                    e.push(`...`);
                    for (let t = this.totalPages - 4; t <= this.totalPages; t++)
                        e.push(t);
                } else {
                    e.push(`...`);
                    for (let t = this.currentPage - 1; t <= this.currentPage + 1; t++)
                        e.push(t);
                    (e.push(`...`), e.push(this.totalPages));
                }
                return e;
            },
            goToPage(e) {
                e >= 1 && e <= this.totalPages && (this.currentPage = e);
            },
        };
    }),
        t.data(`productForm`, () => ({
            form: {
                name: ``,
                sku: ``,
                category: ``,
                price: ``,
                stock: ``,
                description: ``,
                status: `draft`,
            },
            saveProduct() {
                if (
                    !this.form.name ||
                    !this.form.sku ||
                    !this.form.category ||
                    !this.form.price ||
                    !this.form.stock ||
                    !this.form.status
                ) {
                    alert(`Please fill in all required fields`);
                    return;
                }
                (console.log(`Saving product:`, this.form),
                    f.default === void 0
                        ? alert(`Product saved successfully!`)
                        : f.default.fire({
                            title: `Product Saved!`,
                            text: `The product has been saved successfully`,
                            icon: `success`,
                            confirmButtonText: `OK`,
                        }),
                    (this.form = {
                        name: ``,
                        sku: ``,
                        category: ``,
                        price: ``,
                        stock: ``,
                        description: ``,
                        status: `draft`,
                    }));
            },
        })),
        t.data(`searchComponent`, d({ getResults: () => [] })),
        t.data(`themeSwitch`, () => ({
            currentTheme: `light`,
            init() {
                ((this.currentTheme = localStorage.getItem(`theme`) || `light`),
                    document.documentElement.setAttribute(
                        `data-bs-theme`,
                        this.currentTheme,
                    ));
            },
            toggle() {
                ((this.currentTheme = this.currentTheme === `light` ? `dark` : `light`),
                    document.documentElement.setAttribute(
                        `data-bs-theme`,
                        this.currentTheme,
                    ),
                    localStorage.setItem(`theme`, this.currentTheme));
            },
        })));
});
