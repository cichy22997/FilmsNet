<template>
	<header>
		<div class="pageBanner">
			<img class="logo" src="@/assets/logocircle.png" />
			<h1>FilmsNET</h1>
		</div>
	</header>
</template>

<script>
export default {
	name: 'Header'
}
</script>

<style scoped>
header {
	position: relative;
	top: 0;
	display: flex;
	justify-content: space-between;
	align-items: center;
	background-color: #fff;
	width: 100%;
	min-height: 7vh;
}
.logo {
	width: 70px;
	height: 70px;
}
.pageBanner {
	display: flex;
	justify-content: left;
	align-items: center;
}
.pageBanner > h1 {
	color: #041e62;
	font-size: 24px;
}
</style>
