<template>
    <div class="dropdown">
        <button class="dropdown-button" @click="toggleDropdown">
            {{ buttonText }}
        </button>
        <ul class="dropdown-menu" v-if="isOpen">
            <slot></slot>
        </ul>
    </div>
</template>
  
<script>
export default {
    data() {
        return {
            isOpen: false
        };
    },
    props: {
        buttonText: {
            type: String,
            required: true
        }
    },
    methods: {
        toggleDropdown() {
            this.isOpen = !this.isOpen;
        }
    }
};
</script>
  
<style>
.dropdown {
    position: relative;
    display: inline-block;
}

.dropdown-button {
    background-color: #f8f8f8;
    border: 1px solid #ccc;
    padding: 8px 16px;
    cursor: pointer;
}

.dropdown-menu {
    position: absolute;
    top: 100%;
    left: 0;
    background-color: #f8f8f8;
    border: 1px solid #ccc;
    list-style-type: none;
    padding: 0;
    margin: 0;
    display: none;
}

.dropdown-menu li {
    padding: 8px 16px;
    cursor: pointer;
}

.dropdown-menu li:hover {
    background-color: #e0e0e0;
}

.dropdown-menu li:last-child {
    border-top: 1px solid #ccc;
}

.dropdown-menu li:last-child:hover {
    background-color: #f8f8f8;
}
</style>
  