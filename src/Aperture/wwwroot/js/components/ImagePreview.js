class ImagePreview extends HTMLElement {
    constructor() {
        super();
        this.style = "display: none;";
    }

    connectedCallback() {
        this.addFileControlChangeListener();
    }

    disconnectedCallback() {
        this.removeFileControlEventListener();
    }

    addFileControlChangeListener() {
        const forControlId = this.getAttribute("for");
        const control = document.getElementById(forControlId);

        control.addEventListener('change',
            () => {
                for (let i = this.childNodes.length - 1; i >= 0; i--) {
                    if (this.childNodes[i].tagName === 'IMG') {
                        this.removeChild(this.childNodes[i]);
                    }
                }
                if (control.files && control.files.length > 0) {
                    const className = this.getAttribute("data-class");
                    const style = this.getAttribute("data-style");
                    const img = document.createElement('img');
                    img.className = className;
                    this.appendChild(img);

                    const reader = new FileReader();
                    reader.onload = () => {
                        img.src = reader.result;
                        img.alt = control.files[0].name;
                        img.style = style;
                        img.class = className;
                        this.style = "display: inline-block";
                    };
                    reader.readAsDataURL(control.files[0]);
                } else {
                    this.style = "display: none;";
                }
            });
    }

    removeFileControlEventListener() {
        const forControlId = this.getAttribute("for");
        const control = document.getElementById(forControlId);

        control.removeEventListener();
    }
}

window.customElements.define('image-preview', ImagePreview);