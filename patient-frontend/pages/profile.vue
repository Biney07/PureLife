<template>
    <div class="profile-container">
        <h1>Profile</h1>
        <ProfileCard/>
    </div>
</template>
  
<script>
import ProfileCard from "~/components/ProfileCard";
export default {

    components: {
        ProfileCard,
    },
}
</script>
  
<style scoped>
h1 {
    font-size: 24px;
    color: #333;
    margin-top: 20px;
}
.profile-container{
    display: flex;
    padding: 110px 30px 30px 30px ;
}
</style>
  