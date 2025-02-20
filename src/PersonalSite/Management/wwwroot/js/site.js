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

class API {
    static async fetch(url, options) {

    }
}