window.mermaidInterop = {
    render: async function (id) {
        if (!window.mermaid) {
            return;
        }

        const element = document.getElementById(id);

        if (!element) {
            return;
        }

        await window.mermaid.run({
            nodes: [element]
        });
    }
};
