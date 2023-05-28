<template>
<transition name="modal-fade">
  <div class="modal-backdrop">
    <div class="modal">
      <header class="modal-header">
        <slot name="header">
          This is the default title!
        </slot>
        <button
          type="button"
          class="btn-close"
          @click="close"
        >
          x
        </button>
      </header>

      <section class="modal-body">
        <slot name="body">
          This is the default body!
        </slot>
       </section>

      <footer class="modal-footer">
        <slot name="footer">
          <!--  -->
        </slot>
        <p
          type="button"
          class="close-button"
          @click="close"
        >
          Close
        </p>
      </footer>
    </div>
  </div>
</transition>
</template>

<script>
  export default {
    name: 'Modal',
    methods: {
      close() {
        this.$emit('close');
      },
    },
  };
</script>

<style>
  .modal-backdrop {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal {
    background: #FFFFFF;
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
    overflow-x: auto;
    display: flex;
    flex-direction: column;
    top: 10%;
    left: 30%;
    width: 50%;
    height: 80%;
    border: none;
    outline: none;
    border-radius: 8px;
  }

  .modal-header,
  .modal-footer {
    padding: 15px;
    display: flex;
    background: #fafafa;
  }

  .modal-header {
    position: relative;
    border-bottom: 1px solid #eeeeee;
    justify-content: space-between;
    align-items: center;
  }

  .modal-footer {
    border-top: 1px solid #eeeeee;
    flex-direction: column;
    justify-content: flex-end;
    align-items: flex-end;
  }

  .modal-body {
    position: relative;
    padding: 20px 10px;
  }

    .close-button{
        background: rgba(252, 146, 120, 0.9);
        width: fit-content;
        padding: 10px 20px;
        border-radius: 10px;
        color: white;
        transition: background-color 0.3s;
        font-size: 1rem;
        font-weight: 500;
    }

    .close-button:hover, .btn-close:hover{
        background: rgba(245, 118, 86, 0.9);
        transition: background-color 0.3s;
        cursor: pointer;
    }

    .btn-close {
        color: white;
        background: rgba(252, 146, 120, 0.9);
        border: none !important;
        outline: none;
        width: fit-content;
        padding: 5px 10px;
        border-radius: 6px;
        transition: background-color 0.3s;
        font-size: 16px;
        font-weight: 500;
    }

    .btn-close:focus {
        border: none !important;
        outline: none;
    }


  .modal-fade-enter,
  .modal-fade-leave-to {
    opacity: 0;
}

  .modal-fade-enter-active,
  .modal-fade-leave-active {
    transition: opacity .2s ease;
  }
</style>