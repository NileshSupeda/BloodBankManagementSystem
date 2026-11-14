(() => {
    "use strict";

    const sidebar = document.getElementById("mainSidebar");
    const sidebarToggle = document.getElementById("sidebarToggle");
    const sidebarBackdrop = document.getElementById("sidebarBackdrop");

    const closeSidebar = () => {
        if (!sidebar || !sidebarToggle || !sidebarBackdrop) return;
        sidebar.classList.remove("is-open");
        sidebarBackdrop.classList.remove("is-visible");
        sidebarToggle.setAttribute("aria-expanded", "false");
        sidebarToggle.setAttribute("aria-label", "Open navigation menu");
        sidebarBackdrop.setAttribute("tabindex", "-1");
        document.body.classList.remove("sidebar-open");
    };

    const openSidebar = () => {
        if (!sidebar || !sidebarToggle || !sidebarBackdrop) return;
        sidebar.classList.add("is-open");
        sidebarBackdrop.classList.add("is-visible");
        sidebarToggle.setAttribute("aria-expanded", "true");
        sidebarToggle.setAttribute("aria-label", "Close navigation menu");
        sidebarBackdrop.setAttribute("tabindex", "0");
        document.body.classList.add("sidebar-open");
    };

    sidebarToggle?.addEventListener("click", () => {
        const isOpen = sidebarToggle.getAttribute("aria-expanded") === "true";
        isOpen ? closeSidebar() : openSidebar();
    });
    sidebarBackdrop?.addEventListener("click", closeSidebar);
    sidebar?.querySelectorAll("a").forEach(link => link.addEventListener("click", closeSidebar));
    document.addEventListener("keydown", event => {
        if (event.key === "Escape") closeSidebar();
    });
    window.addEventListener("resize", () => {
        if (window.matchMedia("(min-width: 901px)").matches) closeSidebar();
    });

    // The shared search filters records/cards on the page currently being viewed.
    const searchInput = document.getElementById("globalSearch");
    if (!searchInput) return;

    const normalize = value => value.toLocaleLowerCase().replace(/\s+/g, " ").trim();
    const searchableSelector = ".stat-card, .quick-action, .blood-type, .summary-item";
    const searchableCards = Array.from(document.querySelectorAll(searchableSelector));
    const tables = Array.from(document.querySelectorAll(".table"));

    if (searchableCards.length === 0 && tables.length === 0) {
        searchInput.closest(".search-box")?.setAttribute("hidden", "hidden");
        return;
    }

    const ensureNoResultsRow = (table, tbody, columnCount) => {
        let row = tbody.querySelector(".no-results-row");
        if (!row) {
            row = document.createElement("tr");
            row.className = "no-results-row";
            const cell = document.createElement("td");
            cell.colSpan = columnCount;
            cell.className = "text-center text-muted py-4";
            cell.textContent = "No records match your search.";
            row.appendChild(cell);
            tbody.appendChild(row);
        }
        return row;
    };

    searchInput.addEventListener("input", () => {
        const query = normalize(searchInput.value);

        searchableCards.forEach(item => {
            item.hidden = query !== "" && !normalize(item.textContent).includes(query);
        });

        tables.forEach(table => {
            const tbody = table.tBodies[0];
            if (!tbody) return;

            const rows = Array.from(tbody.rows).filter(row =>
                !row.classList.contains("empty-state-row") &&
                !row.classList.contains("no-results-row")
            );
            if (rows.length === 0) return;

            let matchCount = 0;
            rows.forEach(row => {
                const matches = query === "" || normalize(row.textContent).includes(query);
                row.hidden = !matches;
                if (matches) matchCount += 1;
            });

            const noResultsRow = ensureNoResultsRow(
                table,
                tbody,
                table.tHead?.rows[0]?.cells.length || rows[0].cells.length
            );
            noResultsRow.hidden = query === "" || matchCount > 0;
        });
    });
})();
