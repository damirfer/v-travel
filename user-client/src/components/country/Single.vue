<template>
    <div 
    @click="$router.push({name: 'CountryDetails', params:{ id: country.CountryId }})"
    class="day-single clear-floats">
        <div>
            <img class="flag-round" :src="country.FlagUrl" alt="Country flag">
        </div>
        <div>
            <p>{{ country.Name }}</p>
        </div>
    </div>
</template>
<script>
export default {
    props: ["country"]
};
</script>
<style scoped>
.day-single {
  padding: 0 15px;
  position: relative;
}
.day-single div {
  float: left;
  padding: 10px 0;
  text-align: left;
}

.flag-round{
    border-radius: 50%;
}

.day-single div:nth-child(1){
    width: 25%;
}
.day-single div:nth-child(1) img{
    width: 50px;
    height: 50px;
    max-width: 150px;
}
.day-single div:nth-child(2){
    width: 70%;
    text-align: left;
    height: 100%;
    position: absolute;
    left: 100px;
    top: 50%;
    transform: translateY(-50%);
}
.day-single div:nth-child(2) p{
    font-family: 'Montserrat', sans-serif;
    font-size: 1.1em;
    margin: 0;
    position: absolute;
    top: 50%;
    transform: translateY(-50%);

}
</style>
