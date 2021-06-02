<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div class="modal">
        <header class="modal-header">
          {{ modalHeader }}
          <div>
            <p class="typo__p submit-error" v-if="submitStatus === 'ERROR'">
              Please fill the form correctly.
            </p>
            <p class="typo__p" v-if="submitStatus === 'OK'"></p>
          </div>
        </header>
        <section class="modal-body">
          <div
            class="form-group"
            :class="{ 'form-group--error': $v.modalTitle.$error }"
          >
            <div
              class="form-group"
              :class="{ 'form-group--error': $v.modalTitle.$error }"
            >
              <input
                class="form-input"
                v-model="modalTitle"
                placeholder="Title"
                v-model.trim="$v.modalTitle.$model"
                @input="setTitle($event.target.value)"
              />
            </div>
            <div class="error" v-if="!$v.modalTitle.required">
              Title is required
            </div>
          </div>
          <div
            class="form-group"
            :class="{ 'form-group--error': $v.modalYear.$error }"
          >
            <input
              class="form-input"
              v-model="modalYear"
              placeholder="Year"
              @change="setYear($event.target.value)"
            />
            <div class="error" v-if="!$v.modalYear.required">
              Year is required
            </div>
            <div class="error" v-if="!$v.modalYear.between">
              Must be between {{ $v.modalYear.$params.between.min }} and
              {{ $v.modalYear.$params.between.max }}
            </div>
          </div>
        </section>
        <footer class="modal-footer">
          <button
            type="button"
            class="btn-close btn-modal anim-btn"
            @click="close"
          >
            Close
          </button>
          <button
            type="button"
            :disabled="submitStatus === 'PENDING'"
            class="btn-submit btn-modal anim-btn"
            @click="submit"
          >
            Submit
          </button>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import { required, between } from "vuelidate/lib/validators";

export default {
  name: "Modal",
  props: {
    modalHeader: String,
    modalTitle: String,
    modalYear: Number,
    modalId: Number,
  },
  data() {
    return {
      submitStatus: null,
    };
  },
  methods: {
    close() {
      this.$emit("close");
    },
    submit() {
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.$emit("submit", this.modalId, this.modalTitle, this.modalYear);
        this.submitStatus = "OK";
      }
    },

    setTitle(value) {
      this.modalTitle = value;
    },

    setYear(value) {
      this.modalYear = value;
    },
  },
  validations: {
    modalTitle: {
      required
    },
    modalYear: {
      required,
      between: between(1900, 2100),
    },
  },
};
</script>

<style>
.submit-error {
  color: #e83538;
}

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
  z-index: 2;
}

.modal {
  background: #ffffff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  z-index: 3;
}

.modal-header,
.modal-footer {
  padding: 15px;
  display: flex;
  flex: wrap;
  direction: column;
  align-items: center;
  justify-content: space-around;
}

.modal-header {
  position: relative;
  border-bottom: 1px solid #eeeeee;
  color: #1c7eff;
  justify-content: space-between;
}

.modal-footer {
  border-top: 1px solid #eeeeee;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}

.modal-body > * {
  flex: 1 1 100%;
  margin: 10px 10px;
  border-radius: 5px;
  padding: 0 10px;
}
.form-input {
  border: 1px solid #f2f2f2;
  min-height: 25px;
  font-size: 16px;
  width: 100%;
  padding: 5px;
  background-color: #e6e6e6;
}

.error {
  color: #e38c34;
  font-size: 12px;
  margin-top: 4px;
}

.btn-modal {
  color: white;
  border: 0;
  width: 40vw;
  height: 30px;
  max-width: 150px;
  border-radius: 5px;
}
.btn-close {
  background: #e83538;
}

.btn-submit {
  background: #4aae9b;
}

.modal-fade-enter,
.modal-fade-leave-to {
  opacity: 0;
}

.modal-fade-enter-active,
.modal-fade-leave-active {
  transition: opacity 0.5s ease;
}

@media only screen and (min-width: 442px) {
  .modal {
    max-width: 440px;
  }
}
</style>
