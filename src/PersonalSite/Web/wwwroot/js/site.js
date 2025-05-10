addEventListener("load", () => {
    document.querySelectorAll(".image-upload-area").forEach(el => {
        const relatedElement = document.querySelector(el.getAttribute("data-related-input"));
        const filenameLabel = document.querySelector(".image-upload-area-filename");
        if (!relatedElement) {
            throw new Error("Cannot find related element");
        }
        el.addEventListener("click", () => {
            relatedElement.click();
        });
        el.addEventListener("dragover", e => {
            e.preventDefault();
            e.stopPropagation();
        });
        el.addEventListener("drop", e => {
            e.preventDefault();
            e.stopPropagation();
            relatedElement.files = e.dataTransfer.files;
            relatedElement.dispatchEvent(new Event("change", { bubbles: true }));
            relatedElement.dispatchEvent(new Event("input", { bubbles: true }));
        });
        relatedElement.addEventListener("input", () => {
            if (relatedElement.files.length) {
                filenameLabel.style.display = "flex";
                filenameLabel.innerHTML = relatedElement.files[0].name;
            }
        });
    });
});

function getAllElementsWithIDs(startQuery="body") {
    const results = {};
    document.querySelectorAll(`${startQuery} [id]`).forEach(el => {
        results[el.id] = el;
    });
    return results;
}

// TODO?: pull dialog library over from other project, if
// the need is there...
class Dialog {

    static layers = [];

    static createLayer() {
        const layer = document.createElement("div");
        layer.className = "dialog-layer";
        document.body.appendChild(layer);
        return layer;
    }
    static show(element) {
        const layer = this.createLayer();
        layer.appendChild(element);
        this.layers.push(layer);
    }
    static close() {
        if (!this.layers.length) {
            throw new Error("No dialog layer to close!");
        }
        this.layers.pop().remove();
    }
}

function cloneTemplate(templateID) {
    const template = document.getElementById(templateID);
    if (!template) {
        throw new Error(`Could not find template with ID "${templateID}"`);
    }
    return template.content.cloneNode(true);
}