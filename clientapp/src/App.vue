<template>
  <div class="container">
    <Header />
    <Films
      @add-film="onAddFilm"
      @update-film="onUpdateFilm"
      @delete-film="onDeleteFilm"
      :films="films"
    />
    <Modal
      :modalHeader="modalHeader"
      :modalYear="modalYear"
      :modalTitle="modalTitle"
      :modalId="modalId"
      v-show="isModalVisible"
      @close="closeModal"
      @submit="submitModal"
    />
    <Footer />
  </div>
</template>

<script>
import Modal from "./components/Modal.vue";
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import Films from "./components/Films.vue";
import axios from "axios";

export default {
  name: "App",
  components: {
    Header,
    Footer,
    Films,
    Modal,
  },
  data() {
    return {
      films: [],
      isModalVisible: false,
      modalHeader: "",
      modalId: null,
      modalTitle: null,
      modalYear: null,
      isModalAdding: false,
      isModalUpdating: false,
      loading: true,
    };
  },
  methods: {
    async onDeleteFilm(id, index) {
      if (confirm("Do you want to delete " + this.films[index].Title + "?")) {
        await this.deleteFilm(id);
        await this.getFilmsList();
      } else return;
    },
    onUpdateFilm(id, index) {
      this.isModalUpdating = true;
      this.isModalVisible = true;
      var exactFilm = this.films[index];
      this.modalId = id;
      this.modalTitle = exactFilm.title;
      this.modalYear = exactFilm.year;
      this.modalHeader = "Update Film";
    },
    onAddFilm() {
      this.isModalAdding = true;
      this.isModalVisible = true;
      this.modalTitle = "";
      this.modalYear = null;
      this.modalHeader = "Add new Film";
    },
    closeModal() {
      this.isModalVisible = false;
    },
    async submitModal(id, title, year) {
      if (this.isModalAdding) {
        await this.addFilm(title, year);
        await this.getFilmsList();
        this.isModalAdding = false;
      } else if (this.isModalUpdating) {
        await this.updateFilm(id, title, year);
        await this.getFilmsList();
        this.isModalUpdating = false;
      }
      this.closeModal();
    },
    async getFilmsList() {
        await axios
		.get("/api/GetFilms")
		.then(response => {
			this.films = JSON.parse(response.data);
		})
    },
    async addFilm(title, year) {
        await axios
          .post("/api/AddFilm/"+title+"/"+year)
          .catch(function (error) {
            alert(error);
          });
    },
    async updateFilm(id, title, year) {
        await axios
          .patch("/api/PatchFilm/"+id+"/"+title+"/"+year)
          .catch(function (error) {
            alert(error);
          });
    },
    async deleteFilm(id) {
      await axios
        .delete("/api/DeleteFilm/" + id)
        .catch(function (error) {
          alert(error);
        });
    },
  },
  mounted() {
    this.getFilmsList();
  },
};
</script>

<style>
body {
  background-color: #dbdbdb;
  font-family: "Lato", sans-serif;
  margin: 0;
}

.btn {
  width: 80px;
  height: 30px;
  border: 0;
  color: #fff;
  border-radius: 5px;
}
.anim-btn{
  transition: transform 0.2s ease-in-out;
}
.anim-btn:hover {
	transform: scale(0.9);
	cursor: pointer;
}

.container {
}

.container > * {
}
</style>
