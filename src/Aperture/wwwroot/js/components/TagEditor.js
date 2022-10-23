class TagEditor extends HTMLElement {
    constructor() {
        super();
        this.data = [];
        this.tagClass = this.getAttribute("tagClass") ?? "btn btn-primary m-1";
        this.inputClass = this.getAttribute("inputClass")??"form-control mt-3";
        this.containerClass = this.getAttribute("containerClass")??"col-md-6";
        this.backingControlId = this.getAttribute("for");
        this.backingControl = document.getElementById(this.backingControlId);
        this.container = document.createElement("div");
        this.container.className = this.containerClass;
        this.tagContainer = document.createElement("div");
        this.container.appendChild(this.tagContainer);
        this.inputContainer = document.createElement("div");
        this.inputBox = document.createElement("input");
        this.inputBox.type = "text";
        this.inputBox.className = this.inputClass;
        this.inputContainer.appendChild(this.inputBox);
        this.container.appendChild(this.inputContainer);
        this.appendChild(this.container);
    }

    connectedCallback() {
        let initData = JSON.parse(this.backingControl.value);
        for (let i = 0; i < initData.length; i++) {
            this.addTag(initData[i]);
        }

        this.inputBox.addEventListener("keyup", (event) => {
            event.preventDefault();
            if (event.keyCode === 13) {
                this.addTag(this.inputBox.value);
                this.inputBox.value = "";
            }
        });
    }

    addTag(value) {
        this.data.push(value);
        if (this.backingControl) {
            this.backingControl.value = JSON.stringify(this.data);
        }
        const tag = document.createElement("span");
        tag.innerHTML = value;
        tag.className = this.tagClass;
        this.tagContainer.appendChild(tag);

        tag.addEventListener("click", (event) => {
            this.data = this.data.filter(a => a !== event.target.innerHTML);
            this.backingControl.value = JSON.stringify(this.data);
            this.tagContainer.removeChild(event.target);
        });
    }
}

window.customElements.define('tag-editor', TagEditor);

