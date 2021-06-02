<template>
	<section class="content">
		<div class="toolbar-sticky">
			<button @click="onClick()" class="add-btn anim-btn">
				<img src="@/assets/add-icon.png" />
			</button>
		</div>

		<div class="films">
			<div style="overflow-x: auto">
				<table>
					<thead>
						<th>#</th>

						<th>Title</th>

						<th>Year</th>

						<th>Action</th>
					</thead>

					<tbody>
						<tr :key="film.id" v-for="(film, index) in films">
							<th>{{ index + 1 }}</th>

							<th>{{ film.Title }}</th>

							<th>{{ film.Year }}</th>

							<th>
								<div>
									<Button
										text="Update"
										color="#347ae3"
										@onClick="$emit('update-film', film.Id, index)"
									/>

									<Button
										text="Delete"
										color="#e83538"
										@onClick="$emit('delete-film', film.Id, index)"
									/>
								</div>
							</th>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
	</section>
</template>

<script>
import Button from './Button.vue'
export default {
	name: 'Films',
	components: {
		Button
	},
	props: {
		films: Array
	},
	methods: {
		onClick() {
			this.$emit('add-film')
		}
	}
}
</script>

<style scoped>
.content {
	display: flex;
	justify-content: center;
	align-items: flex-start;
	flex-direction: row;
	flex-wrap: wrap;
	min-height: 87vh;
	width: 100%;
}

.toolbar-sticky {
	width: 100%;
	align-self: flex-start;
	min-height: 4vh;
	background-color: #fff;
	position: sticky;
	position: -webkit-sticky;
	top: 0;
	display: flex;
	align-items: center;
	justify-content: flex-end;
	z-index: 1;
	padding-right: 2vw;
}

.films {
	align-self: top;
	margin: 10vh 40px;
	border-radius: 10px;
	background-color: #fff;
	min-width: 80vw;
	padding: 20px;
	position: relative;

}

.add-btn {
	background-color: transparent;
	border: 0;
}

table {
	border-collapse: collapse;
	border-spacing: 0;
	width: 100%;
}

th,
td {
	text-align: justify;
	padding: 5px;
	max-width: 300px;
}

tr:nth-child(even) {
	background-color: #f2f2f2;
}

th > div {
	display: flex;
	justify-content: space-around;
	align-items: center;
	flex-wrap: wrap;

}

@media only screen and (min-width: 800px) {
	.films {
		min-width: 640px;
		max-width: 800px;
	}
}

@media only screen and (max-width: 320px) {
	.films {
		padding: 5px;
	}
	th,
	td {
	padding: 2px;
	max-width: 50px;
}
}
</style>
